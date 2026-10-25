






const STATES_VISIBLE_FROM_ZOOM = 4;

export async function initMap(containerId) {
    if (!document.getElementById("maplibre-gl-css")) {
        const link = document.createElement("link");
        link.id = "maplibre-gl-css";
        link.rel = "stylesheet";
        link.href = "https://unpkg.com/maplibre-gl@4.7.1/dist/maplibre-gl.css";
        document.head.appendChild(link);
    }

    if (!window.maplibregl) {
        await new Promise((resolve, reject) => {
            const script = document.createElement("script");
            script.src = "https://unpkg.com/maplibre-gl@4.7.1/dist/maplibre-gl.js";
            script.async = true;
            script.onload = resolve;
            script.onerror = reject;
            document.head.appendChild(script);
        });
    }

    const map = new maplibregl.Map({
        container: containerId,
        style: {
            version: 8,
            glyphs: 'https://demotiles.maplibre.org/font/{fontstack}/{range}.pbf',
            sources: {
                'countries': {
                    type: 'geojson',
                    data: 'https://raw.githubusercontent.com/johan/world.geo.json/master/countries.geo.json'
                },
                'states': {
                    type: 'geojson',
                    data: {
                        type: 'FeatureCollection',
                        features: []
                    }
                }
            },
            layers: [
                {
                    id: 'background',
                    type: 'background',
                    paint: { 'background-color': '#111827' }
                },
                {
                    id: 'country-fills',
                    type: 'fill',
                    source: 'countries',
                    paint: {
                        'fill-color': [
                            'match',
                            ['get', 'id'],
                            'DEU', '#3b82f6',
                            'USA', '#10b981',
                            'CHN', '#f59e0b',
                            'RUS', '#ef4444',
                            'BRA', '#8b5cf6',
                            '#374151'
                        ],
                        'fill-opacity': 0.85
                    }
                },
                {
                    id: 'country-borders',
                    type: 'line',
                    source: 'countries',
                    paint: {
                        'line-color': '#9ca3af',
                        'line-width': 0.5,
                        'line-opacity': 0.3
                    }
                },
                {
                    id: 'country-labels',
                    type: 'symbol',
                    source: 'countries',
                    paint: {
                        'text-color': '#ffffff',
                        'text-halo-color': '#000000',
                        'text-halo-width': 1.5,
                        'text-opacity': 1
                    },
                    layout: {
                        'text-field': ['get', 'name'],
                        'text-font': ['Noto Sans Bold'],
                        'text-size': 11,
                        'text-anchor': 'center',
                        'text-allow-overlap': true,
                        'text-ignore-placement': true,
                        'symbol-placement': 'point'
                    }
                },
                {
                    id: 'state-borders',
                    type: 'line',
                    source: 'states',
                    paint: {
                        'line-color': '#f9fafb',
                        'line-width': [
                            'interpolate',
                            ['linear'],
                            ['zoom'],
                            4, 0.8,
                            8, 2
                        ],
                        'line-opacity': 0
                    }
                },
                {
                    id: 'state-labels',
                    type: 'symbol',
                    source: 'states',
                    paint: {
                        'text-color': '#ffffff',
                        'text-halo-color': '#000000',
                        'text-halo-width': 1.5,
                        'text-opacity': 0
                    },
                    layout: {
                        'text-field': [
                            'coalesce',
                            ['get', 'name'],
                            ['get', 'shapeName'],
                            ''
                        ],
                        'text-font': ['Noto Sans Bold'],
                        'text-size': [
                            'interpolate',
                            ['linear'],
                            ['zoom'],
                            4, 10,
                            8, 14
                        ],
                        'text-anchor': 'center',
                        'text-allow-overlap': true,
                        'text-ignore-placement': true,
                        'symbol-placement': 'point'
                    }
                }
            ]
        },
        center: [10, 50],
        zoom: 1.5,
        projection: 'globe'
    });

    map.addControl(new maplibregl.NavigationControl());

    addZoomLabel(map, containerId);

    window[containerId] = map;

    await loadAllStates(map);

    updateLayerOpacity(map);
    map.on('zoom', () => updateLayerOpacity(map));
    map.on('move', () => updateLayerOpacity(map));
}

async function loadAllStates(map) {
    const statesSource = map.getSource('states');

    if (!statesSource) {
        console.error("Die GeoJSON-Source 'states' wurde nicht gefunden.");
        return;
    }

    const response = await fetch(
        'https://raw.githubusercontent.com/nvkelso/natural-earth-vector/master/geojson/ne_50m_admin_1_states_provinces.geojson'
    );

    if (!response.ok) {
        console.error('Admin-1-GeoJSON konnte nicht geladen werden:', response.status);
        return;
    }

    const statesGeoJson = await response.json();

    if (!statesGeoJson?.features?.length) {
        console.error('Admin-1-GeoJSON enthält keine Features.');
        return;
    }

    statesSource.setData(statesGeoJson);
}

function updateLayerOpacity(map) {
    const statesMode = map.getZoom() >= STATES_VISIBLE_FROM_ZOOM;

    setPaintOpacity(map, 'country-fills', 'fill-opacity', statesMode ? 0 : 0.85);
    setPaintOpacity(map, 'country-borders', 'line-opacity', statesMode ? 0 : 0.3);
    setPaintOpacity(map, 'country-labels', 'text-opacity', statesMode ? 0 : 1);

    setPaintOpacity(map, 'state-borders', 'line-opacity', statesMode ? 0.9 : 0);
    setPaintOpacity(map, 'state-labels', 'text-opacity', statesMode ? 1 : 0);
}

function setPaintOpacity(map, layerId, property, value) {
    if (map.getLayer(layerId)) {
        map.setPaintProperty(layerId, property, value);
    }
}

function addZoomLabel(map, containerId) {
    const container = document.getElementById(containerId);

    if (!container) {
        return;
    }

    const zoomLabel = document.createElement('div');
    zoomLabel.id = `${containerId}-zoom-label`;
    zoomLabel.textContent = 'Zoom: 1.50';

    Object.assign(zoomLabel.style, {
        position: 'absolute',
        top: '10px',
        left: '10px',
        zIndex: '10',
        padding: '6px 10px',
        borderRadius: '6px',
        backgroundColor: 'rgba(17, 24, 39, 0.85)',
        color: '#ffffff',
        fontFamily: 'system-ui, sans-serif',
        fontSize: '12px',
        fontWeight: '600',
        pointerEvents: 'none',
        boxShadow: '0 2px 6px rgba(0, 0, 0, 0.35)'
    });

    container.style.position = 'relative';
    container.appendChild(zoomLabel);

    const updateZoomLabel = () => {
        zoomLabel.textContent = `Zoom: ${map.getZoom().toFixed(2)}`;
    };

    map.on('zoom', updateZoomLabel);
    map.on('move', updateZoomLabel);
    updateZoomLabel();
}

export function destroyMap(containerId) {
    const zoomLabel = document.getElementById(`${containerId}-zoom-label`);

    if (zoomLabel) {
        zoomLabel.remove();
    }

    if (window[containerId]) {
        window[containerId].remove();
        delete window[containerId];
    }
}














// export async function initMap(containerId) {
//     if (!document.getElementById("maplibre-gl-css")) {
//         const link = document.createElement("link");
//         link.id = "maplibre-gl-css";
//         link.rel = "stylesheet";
//         link.href = "https://unpkg.com/maplibre-gl@4.7.1/dist/maplibre-gl.css";
//         document.head.appendChild(link);
//     }

//     if (!window.maplibregl) {
//         await new Promise((resolve, reject) => {
//             const script = document.createElement("script");
//             script.src = "https://unpkg.com/maplibre-gl@4.7.1/dist/maplibre-gl.js";
//             script.async = true;
//             script.onload = resolve;
//             script.onerror = reject;
//             document.head.appendChild(script);
//         });
//     }

//     const map = new maplibregl.Map({
//         container: containerId,
//         style: {
//             version: 8,
//             glyphs: 'https://demotiles.maplibre.org/font/{fontstack}/{range}.pbf',
//             sources: {
//                 'countries': {
//                     type: 'geojson',
//                     data: 'https://raw.githubusercontent.com/johan/world.geo.json/master/countries.geo.json'
//                 }
//             },
//             layers: [
//                 {
//                     id: 'background',
//                     type: 'background',
//                     paint: { 'background-color': '#111827' }
//                 },
//                 {
//                     id: 'country-fills',
//                     type: 'fill',
//                     source: 'countries',
//                     paint: {
//                         'fill-color': [
//                             'match',
//                             ['get', 'id'],
//                             'DEU', '#3b82f6',
//                             'USA', '#10b981',
//                             'CHN', '#f59e0b',
//                             'RUS', '#ef4444',
//                             'BRA', '#8b5cf6',
//                             '#374151'
//                         ],
//                         'fill-opacity': 0.85
//                     }
//                 },
//                 {
//                     id: 'country-borders',
//                     type: 'line',
//                     source: 'countries',
//                     paint: {
//                         'line-color': '#9ca3af',
//                         'line-width': 0.5
//                     }
//                 },
//                 {
//                     id: 'country-labels',
//                     type: 'symbol',
//                     source: 'countries',
//                     minzoom: 0,
//                     maxzoom: 4.5,
//                     layout: {
//                         'text-field': ['get', 'name'],
//                         'text-font': ['Noto Sans Bold'],
//                         'text-size': 11,
//                         'text-anchor': 'center',
//                         'text-allow-overlap': true,
//                         'text-ignore-placement': true,
//                         'symbol-placement': 'point'
//                     },
//                     paint: {
//                         'text-color': '#ffffff',
//                         'text-halo-color': '#000000',
//                         'text-halo-width': 1.5
//                     }
//                 }
//             ]
//         },
//         center: [10, 50],
//         zoom: 1.5,
//         projection: 'globe'
//     });

//     map.addControl(new maplibregl.NavigationControl());

//     addZoomLabel(map, containerId);

//     window[containerId] = map;

//     await addGermanStates(map);
// }

// async function addGermanStates(map) {
//     const response = await fetch(
//         'https://raw.githubusercontent.com/isellsoap/deutschlandGeoJSON/main/2_bundeslaender/2_hoch.geo.json'
//     );

//     if (!response.ok) {
//         console.error('Bundesländer-GeoJSON konnte nicht geladen werden:', response.status);
//         return;
//     }

//     const statesGeoJson = await response.json();

//     map.addSource('german-states', {
//         type: 'geojson',
//         data: statesGeoJson
//     });

//     map.addLayer({
//         id: 'state-fills',
//         type: 'fill',
//         source: 'german-states',
//         minzoom: 4,
//         paint: {
//             'fill-color': '#000000',
//             'fill-opacity': 0.08
//         }
//     });

//     map.addLayer({
//         id: 'state-borders',
//         type: 'line',
//         source: 'german-states',
//         minzoom: 4,
//         paint: {
//             'line-color': '#f9fafb',
//             'line-width': [
//                 'interpolate',
//                 ['linear'],
//                 ['zoom'],
//                 4, 0.8,
//                 8, 2
//             ],
//             'line-opacity': 0.9
//         }
//     });

//     map.addLayer({
//         id: 'state-labels',
//         type: 'symbol',
//         source: 'german-states',
//         minzoom: 5,
//         layout: {
//             'text-field': ['get', 'name'],
//             'text-font': ['Noto Sans Bold'],
//             'text-size': [
//                 'interpolate',
//                 ['linear'],
//                 ['zoom'],
//                 5, 10,
//                 8, 14
//             ],
//             'text-anchor': 'center',
//             'text-allow-overlap': false,
//             'text-ignore-placement': false,
//             'symbol-placement': 'point'
//         },
//         paint: {
//             'text-color': '#ffffff',
//             'text-halo-color': '#000000',
//             'text-halo-width': 1.5
//         }
//     });
// }

// function addZoomLabel(map, containerId) {
//     const container = document.getElementById(containerId);

//     if (!container) {
//         return;
//     }

//     const zoomLabel = document.createElement('div');
//     zoomLabel.id = `${containerId}-zoom-label`;
//     zoomLabel.textContent = 'Zoom: 1.5';

//     Object.assign(zoomLabel.style, {
//         position: 'absolute',
//         top: '10px',
//         left: '10px',
//         zIndex: '10',
//         padding: '6px 10px',
//         borderRadius: '6px',
//         backgroundColor: 'rgba(17, 24, 39, 0.85)',
//         color: '#ffffff',
//         fontFamily: 'system-ui, sans-serif',
//         fontSize: '12px',
//         fontWeight: '600',
//         pointerEvents: 'none',
//         boxShadow: '0 2px 6px rgba(0, 0, 0, 0.35)'
//     });

//     container.style.position = 'relative';
//     container.appendChild(zoomLabel);

//     const updateZoomLabel = () => {
//         zoomLabel.textContent = `Zoom: ${map.getZoom().toFixed(2)}`;
//     };

//     map.on('zoom', updateZoomLabel);
//     map.on('move', updateZoomLabel);
//     updateZoomLabel();
// }

// export function destroyMap(containerId) {
//     const zoomLabel = document.getElementById(`${containerId}-zoom-label`);

//     if (zoomLabel) {
//         zoomLabel.remove();
//     }

//     if (window[containerId]) {
//         window[containerId].remove();
//         delete window[containerId];
//     }
// }
















// export async function initMap(containerId) {
//     if (!document.getElementById("maplibre-gl-css")) {
//         const link = document.createElement("link");
//         link.id = "maplibre-gl-css";
//         link.rel = "stylesheet";
//         link.href = "https://unpkg.com/maplibre-gl@4.7.1/dist/maplibre-gl.css";
//         document.head.appendChild(link);
//     }

//     if (!window.maplibregl) {
//         await new Promise((resolve, reject) => {
//             const script = document.createElement("script");
//             script.src = "https://unpkg.com/maplibre-gl@4.7.1/dist/maplibre-gl.js";
//             script.async = true;
//             script.onload = resolve;
//             script.onerror = reject;
//             document.head.appendChild(script);
//         });
//     }

//     const map = new maplibregl.Map({
//         container: containerId,
//         style: {
//             version: 8,
//             glyphs: 'https://demotiles.maplibre.org/font/{fontstack}/{range}.pbf',
//             sources: {
//                 'countries': {
//                     type: 'geojson',
//                     data: 'https://raw.githubusercontent.com/johan/world.geo.json/master/countries.geo.json'
//                 }
//             },
//             layers: [
//                 {
//                     id: 'background',
//                     type: 'background',
//                     paint: { 'background-color': '#111827' }
//                 },
//                 {
//                     id: 'country-fills',
//                     type: 'fill',
//                     source: 'countries',
//                     paint: {
//                         'fill-color': [
//                             'match',
//                             ['get', 'id'],
//                             'DEU', '#3b82f6',
//                             'USA', '#10b981',
//                             'CHN', '#f59e0b',
//                             'RUS', '#ef4444',
//                             'BRA', '#8b5cf6',
//                             '#374151'
//                         ],
//                         'fill-opacity': 0.85
//                     }
//                 },
//                 {
//                     id: 'country-borders',
//                     type: 'line',
//                     source: 'countries',
//                     paint: {
//                         'line-color': '#9ca3af',
//                         'line-width': 0.5
//                     }
//                 },
//                 {
//                     id: 'country-labels',
//                     type: 'symbol',
//                     source: 'countries',
//                     layout: {
//                         'text-field': ['get', 'name'],
//                         'text-font': ['Noto Sans Bold'],
//                         'text-size': 11,
//                         'text-anchor': 'center',
//                         'text-allow-overlap': true,
//                         'text-ignore-placement': true,
//                         'symbol-placement': 'point'
//                     },
//                     paint: {
//                         'text-color': '#ffffff',
//                         'text-halo-color': '#000000',
//                         'text-halo-width': 1.5
//                     }
//                 }
//             ]
//         },
//         center: [10, 50],
//         zoom: 1.5,
//         projection: 'globe'
//     });

//     map.addControl(new maplibregl.NavigationControl());
//     window[containerId] = map;
// }

// export function destroyMap(containerId) {
//     if (window[containerId]) {
//         window[containerId].remove();
//         delete window[containerId];
//     }
// }
