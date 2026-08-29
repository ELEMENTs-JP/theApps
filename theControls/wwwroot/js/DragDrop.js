
// Sortable 
export function assignMultipleSortableJS(containerClass, draggableClass, dotNetHelper)
{
    try
    {
        // https://sortablejs.github.io/Sortable/ 
        let elements = document.getElementsByClassName(containerClass);

        for (let i = 0; i < elements.length; i++)
        {
            try
            {
                let ele = elements[i];
                if (ele === null || ele === undefined)
                    continue;

          
                assignSortableJS(ele, draggableClass, dotNetHelper)
            }
            catch (e) {
                alert("Fehler: " + e.stack);
            }
            
        }
    }
    catch (e)
    {
        alert('FAIL : Drag Drop JS : Overall : ' + e);
    }
}
function assignSortableJS(containerElement, dragabbleClass, helper)
{
    try {
        // Link: https://github.com/SortableJS/Sortable
        if (!containerElement)
        {
            alert("Container Element null");
            return;
        }
       

        // Bereits initialisiert?
        if (Sortable.get(containerElement))
        {
            alert("Sortable konnte Container nicht instanziieren");
            return;
        }

        // Init swapThreshold: 0.50,
        let sortableDiv = new Sortable(containerElement, {
            // group: '' + dragabbleClass.toString() + '', // Identifier for multiple divs -> 'shared'

            group: {
                name: dragabbleClass,
                pull: true,
                put: true
            },

            chosenClass: "sortable-chosen",
            ghostClass: 'bg-dragdrop',

            sort: true,  // sorting inside list
            delay: 0, // time in milliseconds to define when the sorting should start
            delayOnTouchOnly: false, // only delay if user is using touch
            touchStartThreshold: 0, // px, how many pixels the point should move before cancelling a delayed drag event
            disabled: false, // Disables the sortable if set to true.
            store: null,  // @see Store
            handle: ".grab",  // Drag handle selector within list items
            draggable: "." + dragabbleClass,  // Specifies which items inside the element should be draggable
            setData: function (dataTransfer, dragEl)
            {
                dataTransfer.setData('Text', dragEl.textContent); // `dataTransfer` object of HTML5 DragEvent
            },

            onMove(evt) {

             
            },


            // Element dragging ended 
            // onEnd: function (evt) {
            async onEnd(evt) {


                try {

                    await updateSorting(evt.from, helper);
                    await updateSorting(evt.to, helper);

                }
                catch (e) {
                    alert("FAIL : DragDropJS : OnDragEnd : " + e.stack);
                }

            
            },
        });
    }
    catch (e)
    {
        alert("FAIL : DragDropJS : all : " + e.stack);
    }

}
async function updateSorting(container, helper)
{
    try {
     

        let items = [];

        let allitems = container.children;

        for (let i = 0; i < allitems.length; i++) {

            let item = allitems[i];

            let zone = getAttributeValue(container, "data-zone");

            if (!item)
                continue;

            items.push(
                {
                    GUID: item.id,
                    Sort: (i + 1).toString(),
                    Zone: zone,
                });
        }

        await helper.invokeMethodAsync("OnDragEnd", items);
    }
    catch (e) {
        alert("FAIL : DragDropJS : OnDragEnd : " + e.stack);
    }
   
}




// Attributes 
function assignAttribute(theNode, attributeName, attributeValue)
{
    try
    {
        var theAttribute = document.createAttribute(attributeName);
        theAttribute.value = attributeValue;
        theNode.setAttributeNode(theAttribute);
    } catch (e)
    {
        console.log("Creation of Attribute: " + e);
    }
}
function getAttributeValue(theNode, attributeName)
{
    try
    {
        var theValue = theNode.getAttribute(attributeName);
        if (theValue !== null)
        {
            return theValue;
        }
    }
    catch (e)
    {
        console.log("Getting an Attribute: " + e);
    }

    return '';
}
