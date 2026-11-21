using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using theInfrastructure;

namespace theInfrastructure
{
    public static partial class Helper
    {
        // Reporting 


        public static readonly HashSet<SRD> ReportDefinitions = new()
    {
        // Kundenanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenanalyse",
            Title = "Kundensegmentierung",
            Description = "Erstellen Sie eine Kundensegmentierung, indem Sie Ihre Kunden anhand gemeinsamer Merkmale wie Branche, Unternehmensgröße, Demografie, Kaufverhalten oder Bedürfnissen gruppieren."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenanalyse",
            Title = "Persona-Analyse",
            Description = "Erstellen Sie Personas für die wichtigsten Zielgruppen und beschreiben Sie deren Ziele, Herausforderungen, Erwartungen und Entscheidungsprozesse."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenanalyse",
            Title = "Customer Journey Analyse",
            Description = "Analysieren Sie die einzelnen Kontaktpunkte Ihrer Kunden entlang ihrer Reise vom ersten Interesse bis zur langfristigen Kundenbindung."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenanalyse",
            Title = "Jobs-to-be-Done Analyse",
            Description = "Identifizieren Sie die Aufgaben und Probleme, die Kunden mit Ihrem Produkt oder Ihrer Dienstleistung lösen möchten."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenanalyse",
            Title = "Kaufverhaltensanalyse",
            Description = "Untersuchen Sie Kaufmuster, Entscheidungsfaktoren und Auslöser, die zur Kaufentscheidung Ihrer Kunden führen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenanalyse",
            Title = "Kundenwertanalyse",
            Description = "Bewerten Sie den wirtschaftlichen Wert verschiedener Kundengruppen anhand von Umsatz, Profitabilität und langfristigem Potenzial."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenanalyse",
            Title = "Pain-Point Analyse",
            Description = "Ermitteln Sie die größten Probleme, Frustrationen und Hindernisse Ihrer Kunden bei der Nutzung bestehender Lösungen."
        },

        // Wertangebot
        new SRD
        {
            App = "Business MODELer",
            Group = "Wertangebot",
            Title = "Value Proposition Analyse",
            Description = "Beschreiben Sie den konkreten Nutzen Ihres Angebots und analysieren Sie, welche Kundenprobleme und Bedürfnisse damit gelöst werden."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Wertangebot",
            Title = "Nutzen-Kosten Analyse",
            Description = "Vergleichen Sie den wahrgenommenen Nutzen Ihres Angebots mit den Kosten und Aufwänden für den Kunden."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Wertangebot",
            Title = "Differenzierungsanalyse",
            Description = "Analysieren Sie die Unterschiede zwischen Ihrem Angebot und den Lösungen Ihrer Wettbewerber."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Wertangebot",
            Title = "USP Analyse",
            Description = "Identifizieren Sie die einzigartigen Merkmale und Alleinstellungsmerkmale Ihres Angebots."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Wertangebot",
            Title = "Wettbewerbsvergleich",
            Description = "Vergleichen Sie Funktionen, Nutzenversprechen, Preise und Kundennutzen mit relevanten Wettbewerbern."
        },

        // Markt- und Wettbewerbsanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Markt- und Wettbewerbsanalyse",
            Title = "SWOT Analyse",
            Description = "Analysieren Sie die Stärken, Schwächen, Chancen und Risiken Ihres Geschäftsmodells."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Markt- und Wettbewerbsanalyse",
            Title = "Porter's Five Forces",
            Description = "Bewerten Sie Wettbewerbsintensität, Verhandlungsmacht von Kunden und Lieferanten sowie Bedrohungen durch neue Marktteilnehmer und Ersatzprodukte."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Markt- und Wettbewerbsanalyse",
            Title = "Benchmarking",
            Description = "Vergleichen Sie Ihr Unternehmen systematisch mit führenden Wettbewerbern und Branchenstandards."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Markt- und Wettbewerbsanalyse",
            Title = "Konkurrenz-Mapping",
            Description = "Visualisieren Sie die Position wichtiger Wettbewerber anhand relevanter Merkmale wie Preis, Qualität oder Innovation."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Markt- und Wettbewerbsanalyse",
            Title = "Blue Ocean Analyse",
            Description = "Identifizieren Sie Marktbereiche mit geringer Konkurrenz und entwickeln Sie neue Wertangebote."
        },

        // Erlösmodellanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Erlösmodellanalyse",
            Title = "Revenue Stream Analyse",
            Description = "Analysieren Sie sämtliche Einnahmequellen und bewerten Sie deren Bedeutung für den Unternehmenserfolg."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Erlösmodellanalyse",
            Title = "Preismodellanalyse",
            Description = "Bewerten Sie bestehende Preisstrategien und prüfen Sie alternative Abrechnungsmodelle."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Erlösmodellanalyse",
            Title = "Deckungsbeitragsanalyse",
            Description = "Ermitteln Sie den Beitrag einzelner Produkte oder Dienstleistungen zur Deckung der Fixkosten und zur Gewinnerzielung."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Erlösmodellanalyse",
            Title = "Break-Even Analyse",
            Description = "Berechnen Sie den Punkt, ab dem Erlöse die gesamten Kosten decken."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Erlösmodellanalyse",
            Title = "Szenarioanalyse Umsatz",
            Description = "Bewerten Sie verschiedene Umsatzszenarien und deren Auswirkungen auf die Wirtschaftlichkeit."
        },

        // Kostenstrukturanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Kostenstrukturanalyse",
            Title = "Kostenartenanalyse",
            Description = "Analysieren Sie die Zusammensetzung Ihrer Kosten nach Kategorien und deren Bedeutung."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kostenstrukturanalyse",
            Title = "Kostentreiberanalyse",
            Description = "Identifizieren Sie die wichtigsten Faktoren, die Kosten verursachen oder beeinflussen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kostenstrukturanalyse",
            Title = "Fixkostenanalyse",
            Description = "Bewerten Sie die Struktur und Höhe Ihrer festen Kosten sowie deren Optimierungspotenzial."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kostenstrukturanalyse",
            Title = "Variable Kostenanalyse",
            Description = "Untersuchen Sie die variablen Kosten und deren Entwicklung bei steigender Nachfrage."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kostenstrukturanalyse",
            Title = "Einsparpotenzialanalyse",
            Description = "Identifizieren Sie Bereiche zur Kostensenkung ohne wesentliche Beeinträchtigung des Kundennutzens."
        },

        // Aktivitäten- und Prozessanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Aktivitäten- und Prozessanalyse",
            Title = "Prozessanalyse",
            Description = "Dokumentieren und bewerten Sie die wichtigsten Geschäftsprozesse hinsichtlich Effizienz und Qualität."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Aktivitäten- und Prozessanalyse",
            Title = "Wertstromanalyse",
            Description = "Analysieren Sie alle Schritte der Wertschöpfungskette und identifizieren Sie Verschwendung."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Aktivitäten- und Prozessanalyse",
            Title = "Bottleneck Analyse",
            Description = "Identifizieren Sie Engpässe, welche die Leistungsfähigkeit des Unternehmens begrenzen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Aktivitäten- und Prozessanalyse",
            Title = "Automatisierungspotenzialanalyse",
            Description = "Bewerten Sie Prozesse hinsichtlich Automatisierungs- und Digitalisierungsmöglichkeiten."
        },

        // Ressourcenanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Ressourcenanalyse",
            Title = "VRIO Analyse",
            Description = "Bewerten Sie Ressourcen hinsichtlich Wert, Seltenheit, Imitierbarkeit und organisatorischer Nutzung."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Ressourcenanalyse",
            Title = "Kompetenzanalyse",
            Description = "Analysieren Sie die Kernkompetenzen des Unternehmens und deren strategische Bedeutung."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Ressourcenanalyse",
            Title = "Skill-Gap Analyse",
            Description = "Ermitteln Sie fehlende Fähigkeiten und Kompetenzen im Vergleich zu den zukünftigen Anforderungen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Ressourcenanalyse",
            Title = "Technologieanalyse",
            Description = "Bewerten Sie die vorhandenen Technologien hinsichtlich Zukunftsfähigkeit und Wettbewerbsrelevanz."
        },

        // Partneranalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Partneranalyse",
            Title = "Stakeholder Analyse",
            Description = "Identifizieren Sie relevante Stakeholder und bewerten Sie deren Einfluss und Interessen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Partneranalyse",
            Title = "Lieferantenanalyse",
            Description = "Bewerten Sie Lieferanten hinsichtlich Qualität, Zuverlässigkeit, Kosten und Risiken."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Partneranalyse",
            Title = "Abhängigkeitsanalyse",
            Description = "Identifizieren Sie kritische Abhängigkeiten von Partnern, Lieferanten oder Plattformen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Partneranalyse",
            Title = "Partnerportfolio Analyse",
            Description = "Bewerten Sie bestehende Partner nach strategischer Bedeutung und Nutzen."
        },

        // Risikoanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Risikoanalyse",
            Title = "Risikomatrix",
            Description = "Bewerten Sie Risiken anhand ihrer Eintrittswahrscheinlichkeit und potenziellen Auswirkungen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Risikoanalyse",
            Title = "PESTEL Analyse",
            Description = "Analysieren Sie politische, wirtschaftliche, gesellschaftliche, technologische, ökologische und rechtliche Einflussfaktoren."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Risikoanalyse",
            Title = "Szenarioanalyse Risiken",
            Description = "Entwickeln Sie unterschiedliche Zukunftsszenarien und bewerten Sie deren Auswirkungen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Risikoanalyse",
            Title = "Lieferkettenrisikoanalyse",
            Description = "Bewerten Sie Risiken innerhalb der Lieferkette und entwickeln Sie Gegenmaßnahmen."
        },

        // Innovationsanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Innovationsanalyse",
            Title = "Disruptionsanalyse",
            Description = "Bewerten Sie Technologien oder Marktveränderungen, die das Geschäftsmodell bedrohen oder verändern könnten."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Innovationsanalyse",
            Title = "Trendanalyse",
            Description = "Identifizieren und bewerten Sie relevante technische, gesellschaftliche und wirtschaftliche Trends."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Innovationsanalyse",
            Title = "Technology Radar",
            Description = "Bewerten Sie neue Technologien hinsichtlich Reifegrad, Chancen und strategischer Relevanz."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Innovationsanalyse",
            Title = "Business Model Innovation Analyse",
            Description = "Untersuchen Sie bestehende Geschäftsmodelle auf Potenziale für Innovation und Neugestaltung."
        },

        // Kanäle
        new SRD
        {
            App = "Business MODELer",
            Group = "Kanäle",
            Title = "Kanalinventur",
            Description = "Identifizieren Sie alle bestehenden Kanäle zur Kundenansprache, Vermarktung, Lieferung und Betreuung Ihrer Produkte oder Dienstleistungen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kanäle",
            Title = "Kanal-Wirksamkeitsanalyse",
            Description = "Bewerten Sie die Effektivität der einzelnen Kanäle hinsichtlich Reichweite, Conversion, Kundenzufriedenheit und Kosten."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kanäle",
            Title = "Customer Journey Kanalanalyse",
            Description = "Ordnen Sie die genutzten Kanäle den einzelnen Phasen der Customer Journey zu und identifizieren Sie Optimierungspotenziale."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kanäle",
            Title = "Omnichannel Analyse",
            Description = "Analysieren Sie die Konsistenz und das Zusammenspiel aller Marketing-, Vertriebs- und Servicekanäle."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kanäle",
            Title = "Digitale Kanalstrategie",
            Description = "Bewerten Sie den Einsatz digitaler Kanäle und entwickeln Sie Empfehlungen für neue digitale Vertriebsmöglichkeiten."
        },

        // Kundenbeziehungen
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenbeziehungen",
            Title = "Kundenbindungsanalyse",
            Description = "Bewerten Sie bestehende Maßnahmen zur langfristigen Kundenbindung und deren Wirksamkeit."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenbeziehungen",
            Title = "Kundeninteraktionsanalyse",
            Description = "Analysieren Sie alle Berührungspunkte zwischen Unternehmen und Kunden während des gesamten Kundenlebenszyklus."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenbeziehungen",
            Title = "Self-Service Analyse",
            Description = "Bewerten Sie bestehende Self-Service-Angebote und deren Akzeptanz durch die Kunden."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenbeziehungen",
            Title = "Personalisierungsanalyse",
            Description = "Untersuchen Sie Möglichkeiten zur Individualisierung von Produkten, Dienstleistungen und Kundenkommunikation."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Kundenbeziehungen",
            Title = "Kundenzufriedenheitsanalyse",
            Description = "Analysieren Sie Kundenzufriedenheit, Beschwerden, Feedback und Verbesserungspotenziale."
        },

        // Canvas-Gesamtanalyse
        new SRD
        {
            App = "Business MODELer",
            Group = "Business Model Canvas",
            Title = "Canvas Konsistenzprüfung",
            Description = "Prüfen Sie, ob alle neun Bausteine des Business Model Canvas logisch zusammenpassen und sich gegenseitig unterstützen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Business Model Canvas",
            Title = "Business Model Stärken-Schwächen Analyse",
            Description = "Identifizieren Sie die stärksten und schwächsten Bestandteile des aktuellen Geschäftsmodells."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Business Model Canvas",
            Title = "Abhängigkeitsanalyse der Canvas-Bausteine",
            Description = "Untersuchen Sie Zusammenhänge und gegenseitige Abhängigkeiten zwischen den einzelnen Canvas-Bereichen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Business Model Canvas",
            Title = "Business Model Innovationspotenziale",
            Description = "Identifizieren Sie Innovationsmöglichkeiten in einzelnen oder mehreren Canvas-Bausteinen."
        },
        new SRD
        {
            App = "Business MODELer",
            Group = "Business Model Canvas",
            Title = "Zielbild Business Model Canvas",
            Description = "Entwerfen Sie ein zukünftiges Zielbild des Geschäftsmodells auf Basis identifizierter Chancen und Risiken."
        },





        // =========================
// PROJEKTMANAGEMENT
// =========================

// Projektinitiierung

new SRD
{
    App = "Projektmanagement",
    Group = "Projektinitiierung",
    Title = "Projektauftrag Analyse",
    Description = "Analysieren Sie den Projektauftrag hinsichtlich Zielsetzung, Umfang, Nutzen und Erfolgskriterien."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektinitiierung",
    Title = "Business Case Analyse",
    Description = "Bewerten Sie die wirtschaftliche Begründung des Projekts anhand von Kosten, Nutzen und strategischer Relevanz."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektinitiierung",
    Title = "Stakeholder Identifikation",
    Description = "Identifizieren Sie alle internen und externen Stakeholder mit Einfluss auf das Projekt."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektinitiierung",
    Title = "Stakeholder Einflussanalyse",
    Description = "Bewerten Sie Einfluss, Erwartungen und Interessen der identifizierten Stakeholder."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektinitiierung",
    Title = "Projektziel Analyse",
    Description = "Prüfen Sie die Projektziele auf Vollständigkeit, Konsistenz und Messbarkeit."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektinitiierung",
    Title = "Projektabgrenzung",
    Description = "Definieren Sie klare Grenzen zwischen Projektumfang und Nicht-Projektumfang."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektinitiierung",
    Title = "Machbarkeitsanalyse",
    Description = "Bewerten Sie technische, organisatorische und wirtschaftliche Machbarkeit des Projekts."
},

// Projektplanung

new SRD
{
    App = "Projektmanagement",
    Group = "Projektplanung",
    Title = "Projektstrukturplan Analyse",
    Description = "Erstellen und bewerten Sie den Projektstrukturplan hinsichtlich Vollständigkeit und Nachvollziehbarkeit."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektplanung",
    Title = "Meilensteinplanung",
    Description = "Definieren und bewerten Sie die wichtigsten Meilensteine des Projekts."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektplanung",
    Title = "Terminplan Analyse",
    Description = "Analysieren Sie Projekttermine hinsichtlich Durchführbarkeit und Risiken."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektplanung",
    Title = "Abhängigkeitsanalyse",
    Description = "Identifizieren Sie Abhängigkeiten zwischen Arbeitspaketen und Projektergebnissen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektplanung",
    Title = "Ressourcenplanung",
    Description = "Bewerten Sie den zukünftigen Ressourcenbedarf des Projekts."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektplanung",
    Title = "Kapazitätsplanung",
    Description = "Vergleichen Sie geplante Projektaufwände mit den verfügbaren Kapazitäten."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektplanung",
    Title = "Projektbudget Planung",
    Description = "Analysieren Sie geplante Kosten und Budgetverteilungen."
},

// Ressourcenmanagement

new SRD
{
    App = "Projektmanagement",
    Group = "Ressourcenmanagement",
    Title = "Ressourcenauslastung",
    Description = "Bewerten Sie die aktuelle und zukünftige Auslastung aller Projektressourcen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Ressourcenmanagement",
    Title = "Ressourcenkonflikte",
    Description = "Identifizieren Sie konkurrierende Anforderungen an Ressourcen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Ressourcenmanagement",
    Title = "Skill-Gap Analyse",
    Description = "Vergleichen Sie verfügbare Kompetenzen mit den Projektanforderungen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Ressourcenmanagement",
    Title = "Schlüsselressourcen Bewertung",
    Description = "Identifizieren Sie kritische Ressourcen mit hoher Projektrelevanz."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Ressourcenmanagement",
    Title = "Vertretungsanalyse",
    Description = "Bewerten Sie Ausfallrisiken und vorhandene Vertretungsregelungen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Ressourcenmanagement",
    Title = "Teamzusammensetzung",
    Description = "Analysieren Sie die Zusammensetzung des Projektteams hinsichtlich Kompetenz und Erfahrung."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Ressourcenmanagement",
    Title = "Ressourcenengpass Analyse",
    Description = "Identifizieren Sie Engpässe, die den Projektfortschritt gefährden."
},

// Risikomanagement

new SRD
{
    App = "Projektmanagement",
    Group = "Risikomanagement",
    Title = "Risikoidentifikation",
    Description = "Ermitteln Sie potenzielle Risiken für Termin, Kosten, Qualität und Umfang."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Risikomanagement",
    Title = "Risikobewertung",
    Description = "Bewerten Sie Risiken anhand von Eintrittswahrscheinlichkeit und Schadenshöhe."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Risikomanagement",
    Title = "Risikomatrix",
    Description = "Klassifizieren Sie alle Risiken innerhalb einer Risikomatrix."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Risikomanagement",
    Title = "Maßnahmenanalyse",
    Description = "Bewerten Sie geplante Gegenmaßnahmen zur Risikoreduzierung."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Risikomanagement",
    Title = "Rest-Risiko Bewertung",
    Description = "Ermitteln Sie verbleibende Risiken nach Umsetzung geplanter Maßnahmen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Risikomanagement",
    Title = "Risiko Trend Analyse",
    Description = "Bewerten Sie die Entwicklung identifizierter Risiken über den Projektverlauf."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Risikomanagement",
    Title = "Chancenanalyse",
    Description = "Identifizieren und bewerten Sie positive Projektrisiken und Opportunitäten."
},

// Kostenmanagement

new SRD
{
    App = "Projektmanagement",
    Group = "Kostenmanagement",
    Title = "Budget Analyse",
    Description = "Vergleichen Sie geplante und verfügbare Budgets."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Kostenmanagement",
    Title = "Kostenabweichungsanalyse",
    Description = "Analysieren Sie Abweichungen zwischen Plan- und Ist-Kosten."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Kostenmanagement",
    Title = "Forecast Analyse",
    Description = "Prognostizieren Sie die voraussichtlichen Projektgesamtkosten."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Kostenmanagement",
    Title = "Earned Value Analyse",
    Description = "Bewerten Sie Projektleistung anhand von Kosten, Fortschritt und Planwerten."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Kostenmanagement",
    Title = "Kostentreiberanalyse",
    Description = "Identifizieren Sie die Hauptursachen für Projektkosten."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Kostenmanagement",
    Title = "Nutzen-Kosten Bewertung",
    Description = "Bewerten Sie Projektkosten im Verhältnis zum erwarteten Nutzen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Kostenmanagement",
    Title = "Budgetrisiko Analyse",
    Description = "Identifizieren Sie Risiken für Budgetüberschreitungen."
},

// Qualitätsmanagement

new SRD
{
    App = "Projektmanagement",
    Group = "Qualitätsmanagement",
    Title = "Qualitätsziele Analyse",
    Description = "Bewerten Sie die definierten Qualitätsziele hinsichtlich Vollständigkeit und Messbarkeit."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Qualitätsmanagement",
    Title = "Anforderungsqualität",
    Description = "Prüfen Sie Anforderungen auf Konsistenz, Eindeutigkeit und Nachvollziehbarkeit."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Qualitätsmanagement",
    Title = "Fehlerursachenanalyse",
    Description = "Identifizieren Sie Ursachen von Qualitätsmängeln im Projekt."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Qualitätsmanagement",
    Title = "Testabdeckungsanalyse",
    Description = "Bewerten Sie den Umfang und die Vollständigkeit geplanter Tests."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Qualitätsmanagement",
    Title = "Review Analyse",
    Description = "Bewerten Sie Ergebnisse von Reviews und Qualitätsprüfungen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Qualitätsmanagement",
    Title = "Maßnahmen Wirksamkeit",
    Description = "Analysieren Sie die Wirksamkeit umgesetzter Qualitätsmaßnahmen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Qualitätsmanagement",
    Title = "Qualitätsrisiken",
    Description = "Identifizieren Sie Risiken mit Auswirkungen auf die Projektqualität."
},

// Projektcontrolling

new SRD
{
    App = "Projektmanagement",
    Group = "Projektcontrolling",
    Title = "Projektfortschritt Analyse",
    Description = "Bewerten Sie den Fortschritt des Projekts anhand definierter Kennzahlen."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektcontrolling",
    Title = "Meilensteinstatus",
    Description = "Analysieren Sie den Erfüllungsgrad aller Projektmeilensteine."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektcontrolling",
    Title = "Terminabweichungen",
    Description = "Bewerten Sie Abweichungen zwischen geplantem und tatsächlichem Terminverlauf."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektcontrolling",
    Title = "Leistungsanalyse",
    Description = "Bewerten Sie Produktivität und Leistung innerhalb des Projekts."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektcontrolling",
    Title = "KPI Analyse",
    Description = "Analysieren Sie projektbezogene Kennzahlen hinsichtlich Zielerreichung."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektcontrolling",
    Title = "Ampelbericht",
    Description = "Bewerten Sie Projektstatus nach definierten Ampelkriterien."
},
new SRD
{
    App = "Projektmanagement",
    Group = "Projektcontrolling",
    Title = "Abweichungsursachenanalyse",
    Description = "Identifizieren Sie Ursachen für festgestellte Projektabweichungen."
}, 




// =========================
// PRODUKTMANAGEMENT
// =========================

// Marktanalyse

new SRD
{
    App = "Produktmanagement",
    Group = "Marktanalyse",
    Title = "Marktgrößenanalyse",
    Description = "Analysieren Sie Marktvolumen, Marktwachstum und Marktpotenzial des Zielmarktes."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Marktanalyse",
    Title = "Wettbewerbsanalyse",
    Description = "Identifizieren und bewerten Sie direkte und indirekte Wettbewerber."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Marktanalyse",
    Title = "Kundenbedarfsanalyse",
    Description = "Ermitteln Sie aktuelle und zukünftige Bedürfnisse Ihrer Zielkunden."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Marktanalyse",
    Title = "Trendanalyse",
    Description = "Identifizieren und bewerten Sie relevante Markt- und Technologietrends."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Marktanalyse",
    Title = "Marktsegmentierungsanalyse",
    Description = "Segmentieren Sie den Zielmarkt anhand relevanter Kundenmerkmale."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Marktanalyse",
    Title = "Substitutionsanalyse",
    Description = "Bewerten Sie alternative Lösungen, die dieselben Kundenprobleme adressieren."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Marktanalyse",
    Title = "Porter's Five Forces",
    Description = "Analysieren Sie die Wettbewerbsintensität und Wettbewerbsdynamik im Markt."
},

// Produktstrategie

new SRD
{
    App = "Produktmanagement",
    Group = "Produktstrategie",
    Title = "Produktvision Analyse",
    Description = "Bewerten Sie die langfristige Vision des Produkts hinsichtlich Klarheit und Umsetzbarkeit."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktstrategie",
    Title = "Produktpositionierung",
    Description = "Definieren Sie die Marktposition des Produkts gegenüber Wettbewerbern."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktstrategie",
    Title = "Zielgruppenanalyse",
    Description = "Analysieren Sie die relevanten Zielgruppen und deren Erwartungen an das Produkt."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktstrategie",
    Title = "Value Proposition Analyse",
    Description = "Bewerten Sie das Nutzenversprechen des Produkts für unterschiedliche Kundengruppen."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktstrategie",
    Title = "USP Analyse",
    Description = "Identifizieren Sie die Alleinstellungsmerkmale des Produkts."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktstrategie",
    Title = "Produktportfolio Einordnung",
    Description = "Bewerten Sie die Rolle des Produkts innerhalb des Produktportfolios."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktstrategie",
    Title = "Strategische Zielanalyse",
    Description = "Prüfen Sie die Übereinstimmung des Produkts mit den Unternehmenszielen."
},

// Produktentwicklung

new SRD
{
    App = "Produktmanagement",
    Group = "Produktentwicklung",
    Title = "Anforderungsanalyse",
    Description = "Erfassen und priorisieren Sie funktionale und nicht-funktionale Anforderungen."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktentwicklung",
    Title = "Feature Priorisierung",
    Description = "Bewerten und priorisieren Sie Produktfunktionen hinsichtlich Nutzen und Aufwand."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktentwicklung",
    Title = "Roadmap Analyse",
    Description = "Bewerten Sie die Produkt-Roadmap hinsichtlich Vollständigkeit und Prioritäten."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktentwicklung",
    Title = "MVP Definition",
    Description = "Ermitteln Sie den minimal notwendigen Funktionsumfang für ein marktfähiges Produkt."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktentwicklung",
    Title = "Technische Abhängigkeitsanalyse",
    Description = "Identifizieren Sie technische Abhängigkeiten und deren Auswirkungen auf die Produktentwicklung."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktentwicklung",
    Title = "Innovationsanalyse",
    Description = "Bewerten Sie Innovationspotenziale innerhalb des Produkts."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktentwicklung",
    Title = "Releaseplanung",
    Description = "Planen und bewerten Sie Produkt-Releases hinsichtlich Nutzen und Risiken."
},

// Kunden- und Nutzeranalyse

new SRD
{
    App = "Produktmanagement",
    Group = "Kunden- und Nutzeranalyse",
    Title = "Persona Analyse",
    Description = "Erstellen und bewerten Sie repräsentative Personas für die Zielgruppen."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Kunden- und Nutzeranalyse",
    Title = "Customer Journey Analyse",
    Description = "Analysieren Sie die Kundenerfahrung entlang aller Berührungspunkte mit dem Produkt."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Kunden- und Nutzeranalyse",
    Title = "Nutzungsverhaltensanalyse",
    Description = "Bewerten Sie Nutzungsdaten zur Identifikation von Nutzungsmustern."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Kunden- und Nutzeranalyse",
    Title = "Churn Analyse",
    Description = "Identifizieren Sie Ursachen für die Abwanderung von Kunden oder Nutzern."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Kunden- und Nutzeranalyse",
    Title = "Kundenfeedback Analyse",
    Description = "Analysieren Sie Kundenfeedback und leiten Sie Verbesserungspotenziale ab."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Kunden- und Nutzeranalyse",
    Title = "Pain Point Analyse",
    Description = "Identifizieren Sie Probleme und Frustrationen während der Produktnutzung."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Kunden- und Nutzeranalyse",
    Title = "Jobs-to-be-Done Analyse",
    Description = "Analysieren Sie die Aufgaben und Ziele, die Kunden mit dem Produkt erreichen möchten."
},

// Wirtschaftlichkeit

new SRD
{
    App = "Produktmanagement",
    Group = "Wirtschaftlichkeit",
    Title = "Business Case Analyse",
    Description = "Bewerten Sie Kosten, Nutzen und Wirtschaftlichkeit des Produkts."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Wirtschaftlichkeit",
    Title = "Deckungsbeitragsanalyse",
    Description = "Ermitteln Sie den wirtschaftlichen Beitrag des Produkts zum Unternehmenserfolg."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Wirtschaftlichkeit",
    Title = "Preisstrategie Analyse",
    Description = "Bewerten Sie die Angemessenheit und Wettbewerbsfähigkeit der Preisstrategie."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Wirtschaftlichkeit",
    Title = "Revenue Stream Analyse",
    Description = "Analysieren Sie sämtliche mit dem Produkt verbundenen Erlösquellen."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Wirtschaftlichkeit",
    Title = "Break-Even Analyse",
    Description = "Ermitteln Sie den Zeitpunkt der Wirtschaftlichkeit des Produkts."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Wirtschaftlichkeit",
    Title = "Kostentreiberanalyse",
    Description = "Identifizieren Sie die wesentlichen Ursachen der Produktkosten."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Wirtschaftlichkeit",
    Title = "ROI Analyse",
    Description = "Bewerten Sie die Kapitalrendite des Produkts auf Basis der Investitionen und Erträge."
},

// Produktlebenszyklus

new SRD
{
    App = "Produktmanagement",
    Group = "Produktlebenszyklus",
    Title = "Lebenszyklusphase Analyse",
    Description = "Bestimmen Sie die aktuelle Phase des Produktlebenszyklus."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktlebenszyklus",
    Title = "Einführungsanalyse",
    Description = "Bewerten Sie die Erfolgsfaktoren und Risiken der Produkteinführung."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktlebenszyklus",
    Title = "Wachstumsanalyse",
    Description = "Analysieren Sie Wachstumspotenziale während der Wachstumsphase."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktlebenszyklus",
    Title = "Reifegradanalyse",
    Description = "Bewerten Sie Marktstellung und Entwicklungsmöglichkeiten in der Reifephase."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktlebenszyklus",
    Title = "Rückgangsanalyse",
    Description = "Analysieren Sie Ursachen eines sinkenden Markterfolgs."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktlebenszyklus",
    Title = "Produktablösungsanalyse",
    Description = "Bewerten Sie Optionen zur Ablösung oder Nachfolge des Produkts."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktlebenszyklus",
    Title = "Portfolio-Migrationsanalyse",
    Description = "Planen Sie die Migration von Kunden auf neue Produkte oder Produktgenerationen."
},

// Produktcontrolling

new SRD
{
    App = "Produktmanagement",
    Group = "Produktcontrolling",
    Title = "Produkt KPI Analyse",
    Description = "Bewerten Sie zentrale Produktkennzahlen hinsichtlich Zielerreichung."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktcontrolling",
    Title = "Adoptionsanalyse",
    Description = "Analysieren Sie die Akzeptanz und Nutzung neuer Produktfunktionen."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktcontrolling",
    Title = "Feature Nutzung Analyse",
    Description = "Bewerten Sie die Nutzung einzelner Produktfunktionen durch die Anwender."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktcontrolling",
    Title = "Zielerreichungsanalyse",
    Description = "Vergleichen Sie definierte Produktziele mit den tatsächlich erreichten Ergebnissen."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktcontrolling",
    Title = "Abweichungsanalyse",
    Description = "Identifizieren Sie Ursachen für Plan-Ist-Abweichungen."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktcontrolling",
    Title = "Erfolgsfaktorenanalyse",
    Description = "Bewerten Sie die wesentlichen Treiber des Produkterfolgs."
},
new SRD
{
    App = "Produktmanagement",
    Group = "Produktcontrolling",
    Title = "Produktstatusbericht",
    Description = "Erstellen Sie eine Gesamtbewertung des Produkts hinsichtlich Markt, Kunden, Finanzen und Strategie."
}








    };
    }

    public class SRD // Simple Report Definition 
    {
        public string App { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}



