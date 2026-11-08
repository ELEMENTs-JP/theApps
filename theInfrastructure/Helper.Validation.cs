using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace theInfrastructure
{
    public static partial class Helper
    {



        public static readonly HashSet<string> CompanyProductNames = new(StringComparer.Ordinal)
        {
            // --- DEUTSCHLAND / HDAX (DAX, MDAX, SDAX) ---
            "SAP", "S/4", "HANA", "HANA", "Siemens", "Energy", "Healthineers", "Digital", "Industries",
            "Allianz", "Volkswagen", "VW", "Golf", "Passat", "Tiguan", "Polo", "ID", "Audi", "A1", "A3", "A4", "A6", "A8", "Q3", "Q5", "Q7", "Q8", "e-tron",
            "Porsche", "911", "Cayenne", "Macan", "Taycan", "Panamera", "Mercedes", "Benz", "EQ", "C-Klasse", "E-Klasse", "S-Klasse",
            "BMW", "Mini", "Rolls-Royce", "i3", "i4", "i7", "iX", "BASF", "Telekom", "T-Mobile", "Magenta", "Bayer", "Aspirin", "Xarelto",
            "Adidas", "Puma", "Infineon", "Aurix", "CoolSiC", "E.ON", "RWE", "DHL", "Deutsche", "Post", "Lufthansa", "Eurowings",
            "Henkel", "Persil", "Pril", "Schwarzkopf", "Loctite", "Beiersdorf", "Nivea", "Eucerin", "La", "Prairie",
            "Zalando", "Vonovia", "Brenntag", "Symrise", "Hannover", "Rück", "Münchener", "Continental", "Conti", "HeidelbergMaterials",
            "Qiagen", "Sartorius", "Zimmer", "Airbus", "A320", "A350", "A380", "Helicopters", "MTU", "Aero", "Engines",
            "Covestro", "Merck", "KGaA", "Rheinmetall", "Leopard", "Boxer", "Skynex", "Carl", "Zeiss", "Meditec",
            "Evonik", "GEA", "Group", "Knorr-Bremse", "LEG", "Immobilien", "Nemetschek", "Vector", "Rational",
            "Scout24", "ImmoScout24", "TAG", "TeamViewer", "Aixtron", "Atoss", "Aurubis", "Basler", "Befesa", "Bilfinger",
            "Cewe", "Dermapharm", "Deutz", "Dürr", "Eckert", "Ziegler", "ElringKlinger", "Encavis", "Energiekontor",
            "Evotec", "Fielmann", "Fraport", "freenet", "FUCHS", "GESCO", "Grammer", "Hamborner", "Hapag-Lloyd",
            "Hella", "Hensoldt", "Hornbach", "Hugo", "Boss", "Indus", "Jenoptik", "Jungheinrich", "KION",
            "Klöckner", "Koenig", "Bauer", "Krones", "KUKA", "Lanxess", "Leifheit", "Metro", "Nagarro", "Nordex",
            "OHB", "Pfeiffer", "Vacuum", "ProSiebenSat1", "SGL", "Carbon", "Sixt", "SMA", "Solar", "Stabilus",
            "Stratec", "Südzucker", "Synlab", "Talanx", "Technotrans", "ThyssenKrupp", "Traton", "Varta", "Verbio",
            "Vossloh", "Wacker", "Chemie", "Neuson", "Washtec", "Westwing", "Wüstenrot", "Württembergische", "Zooplus",

            // --- ERWEITERUNG HDAX (FEHLENDE UNTERNEHMEN & MARKEN) ---
            "Commerzbank", "Börse", "Xetra", "Clearstream", "Delivery", "Hero",
            "Foodpanda", "Talabat", "Fresenius", "Helios", "Vamed", "Daimler",
            "Truck", "Actros", "Atego", "Fuso", "Setra", "TUI", "Cruises",
            "Mein", "Schiff", "CTS", "Eventim", "Dräger", "Drägerwerk",
            "Gerresheimer", "Heidelberger", "Druckmaschinen", "Schaeffler",
            "FAG", "LuK", "INA", "Salzgitter", "RTL", "VOX", "ntv",
            "Borussia", "Dortmund", "BVB", "FlatexDEGIRO", "Flatex",
            "Degiro", "Hypoport", "Klein", "RENK", "Aroundtown", "MLP", "PNE", "Secunet",

            // --- USA / NASDAQ 100 / TECH GIANTS ---
            "Apple", "iPhone", "iPad", "Mac", "MacBook", "iOS", "macOS", "WatchOS", "Vision", "Pro", "AirPods", "App", "Store",
            "Microsoft", "Windows", "Office", "Azure", "Surface", "Xbox", "Copilot", "Teams", "PowerBI", "Visual", "Studio",
            "NVIDIA", "GeForce", "RTX", "GTX", "Blackwell", "Hopper", "CUDA", "Omniverse", "Shield", "Tegra",
            "Amazon", "AWS", "Kindle", "Alexa", "Prime", "Echo", "Fire", "Twitch", "Ring", "Blink",
            "Alphabet", "Google", "Android", "YouTube", "Pixel", "Workspace", "Chrome", "Chromebook", "Gemini", "DeepMind", "Waymo",
            "Tesla", "Cybertruck", "Model", "Roadster", "Supercharger", "Powerwall", "Megapack", "Optimus", "Starlink",
            "Meta", "Facebook", "Instagram", "WhatsApp", "Messenger", "Quest", "Threads", "Llama",
            "Netflix", "Adobe", "Photoshop", "Illustrator", "Premiere", "Acrobat", "InDesign", "Lightroom", "Figma",
            "Intel", "Core", "Xeon", "Arc", "Pentium", "Celeron", "Altera", "Gaudi",
            "AMD", "Radeon", "Ryzen", "Epyc", "Threadripper", "Navi", "Zen", "Xilinx",
            "Cisco", "Catalyst", "Webex", "Meraki", "Duo", "SecurID",
            "Oracle", "Database", "MySQL", "Java", "NetSuite", "Cerner",
            "Salesforce", "Tableau", "Slack", "MuleSoft", "Heroku", "DataCloud",
            "IBM", "RedHat", "Watson", "Power10", "Cloud",
            "Qualcomm", "Snapdragon", "Adreno", "Kryo", "Hexagon",
            "Broadcom", "VMware", "Symantec", "CA", "Technologies", "Brocade",
            "Texas", "Instruments", "TI", "MSP430", "C2000", "Sitara",
            "Micron", "Crucial", "DRAM", "NAND", "HBM3E",
            "Applied", "Materials", "Lam", "Research", "KLA", "ASML", "Synopsys", "Cadence",
            "ServiceNow", "Now", "Platform", "Intuit", "TurboTax", "QuickBooks", "Mailchimp",
            "PayPal", "Venmo", "Braintree", "Uber", "Eats", "Freight", "Airbnb", "Booking", "Kayak", "OpenTable",
            "Palantir", "Foundry", "Gotham", "Apollo", "Block", "Square", "CashApp", "Tidal", "Afterpay",
            "Shopify", "Atlassian", "Jira", "Confluence", "Trello", "Bitbucket", "Loom",
            "CrowdStrike", "Falcon", "Snowflake", "Workday", "Palo", "Alto", "Networks", "Prisma", "Cortex",
            "Fortinet", "FortiGate", "FortiGuard", "Datadog", "Zscaler", "Cloudflare", "MongoDB", "Atlas",
            "MercadoLibre", "PDD", "NetEase", "Baidu", "JD.com", "Marvell", "PACCAR", "Monster", "Beverage",
            "Cintas", "Copart", "Old", "Dominion", "DexCom", "Align", "Technology", "IDEXX", "Ross", "Stores",
            "Lululemon", "O'Reilly", "Marriott", "Kraft", "Heinz", "WBD", "Warner", "Bros", "Discovery", "Charter", "Comcast",

            // --- S&P 500 & DOW JONES (FINANZ, PHARMA, KONSUM, ENERGIE, INDUSTRIE) ---
            "Berkshire", "Hathaway", "Geico", "BNSF", "JPMorgan", "Chase", "Visa", "Mastercard", "UnitedHealth", "Optum",
            "Johnson", "Procter", "Gamble", "P&G", "ExxonMobil", "Exxon", "Chevron", "Home", "Depot",
            "Eli", "Lilly", "Mounjaro", "Zepbound", "Verzenio", "AbbVie", "Humira", "Skyrizi", "Rinvoq",
            "Merck", "Keytruda", "Gardasil", "PepsiCo", "Pepsi", "Frito-Lay", "Quaker", "Gatorade",
            "Coca-Cola", "Coke", "Sprite", "Fanta", "Minute", "Maid", "Bank", "America", "BofA",
            "Thermo", "Fisher", "Scientific", "Costco", "Wholesale", "Abbott", "Laboratories", "FreeStyle", "Ensure",
            "Accenture", "McDonald's", "McCafe", "Wells", "Fargo", "Philip", "Morris", "Marlboro",
            "RTX", "Corporation", "Pratt", "Whitney", "Collins", "Aerospace", "Linde", "Danaher", "Beckman", "Coulter",
            "Honeywell", "Verizon", "Fios", "AT&T", "NextEra", "Energy", "Goldman", "Sachs", "American", "Express", "Amex",
            "Caterpillar", "Cat", "Boeing", "737", "747", "777", "787", "Dreamliner", "Starliner", "3M", "Post-it", "Scotch",
            "Nike", "Jordan", "Converse", "Disney", "ESPN", "Pixar", "Marvel", "Star", "Wars", "Hulu",
            "Walmart", "Sam's", "Club", "Pfizer", "Comirnaty", "Viagra", "Amgen", "Enbrel", "Prolia", "Repatha",
            "Medtronic", "Intuitive", "Surgical", "da", "Vinci", "Cigna", "Elevance", "Health", "CVS", "Aetna",
            "Sherwin-Williams", "Illinois", "Tool", "Works", "ITW", "Eaton", "Emerson", "Electric", "Parker-Hannifin",
            "Stryker", "Becton", "Dickinson", "BD", "Colgate-Palmolive", "Colgate", "Palmolive", "Kimberly-Clark",
            "Kleenex", "Huggies", "General", "Mills", "Cheerios", "Häagen-Dazs", "Conagra", "Brands", "Kellogg's",
            "Kellanova", "Hershey's", "Campbell", "Soup", "Tyson", "Foods", "Sysco", "US", "Foods",
            "Dominion", "Duke", "Southern", "Company", "Sempra", "Exelon", "American", "Electric", "Power", "AEP",
            "Constellation", "Public", "Service", "Enterprise", "PSEG", "Waste", "Management", "Republic", "Services",
            "Union", "Pacific", "CSX", "Norfolk", "UPS", "United", "Parcel", "Service", "FedEx",
            "Lockheed", "Martin", "F-35", "F-16", "C-130", "General", "Dynamics", "Electric", "Boat", "Gulfstream",
            "Northrop", "Grumman", "B-21", "Sentinel", "L3Harris", "HII", "Huntington", "Ingalls", "Textron",
            "Freeport-McMoRan", "Newmont", "Southern", "Copper", "Nucor", "Steel", "Dynamics", "Alcoa",
            "Baker", "Hughes", "Halliburton", "Schlumberger", "SLB", "Valero", "Marathon", "Petroleum", "Phillips", "66",
            "Occidental", "OXY", "Hess", "Devon", "EOG", "Resources", "Pioneer", "Natural",
            "BlackRock", "Aladdin", "Charles", "Schwab", "Interactive", "Brokers", "CME", "Nasdaq", "NYSE",
            "Intercontinental", "Exchange", "ICE", "S&P", "Global", "Moody's", "MSCI", "Equifax", "TransUnion", "Experian",
            "Fiserv", "Global", "Payments", "Adyen", "Stripe",
            "MetLife", "Prudential", "Financial", "AIG", "Travelers", "Chubb", "Allstate", "Progressive", "Loews",
            "Aon", "Marsh", "McLennan", "Willis", "Towers", "Watson", "WTW", "Arthur", "Gallagher",
            "PNC", "U.S.", "Bancorp", "Truist", "State", "Street", "New", "York", "Mellon", "BNY", "Northern", "Trust", "KeyCorp",
            "Fifth", "Third", "Regions", "Financial", "M&T", "Citizens", "Group",
            "Discover", "Capital", "One", "Synchrony", "Ally",
            "Simon", "Property", "Prologis", "Equinix", "Digital", "Realty", "Public", "Storage", "Welltower",
            "Ventas", "Extra", "Space", "AvalonBay", "Communities", "Equity", "Residential", "Essex", "Property",
            "Kimco", "Realty", "Income", "Boston", "Properties", "Vornado", "Alexandria", "Real", "Estate", 

            // --- KI & MACHINE LEARNING ---
            "Anthropic", "Claude", "Mistral", "Cohere", "Perplexity", "Groq", "Cerebras", "SambaNova",
            "Midjourney", "Stability", "Diffusion", "Hugging", "Face", "Pinecone", "LangChain",
            "OpenAI", "ChatGPT", "Sora", "Whisper", "Scale", "AI", "C3", "Veritone", "SoundHound",

            // --- BIOTECH & PHARMA ---
            "BioNTech", "Moderna", "Spikevax", "Roche", "Genentech", "Novartis", "Sanofi",
            "AstraZeneca", "GlaxoSmithKline", "GSK", "Regeneron", "Eylea", "Dupixent",
            "Vertex", "Trikafta", "Illumina", "NovaSeq", "CRISPR", "Therapeutics", "Editas",
            "Medicine", "Beam", "Intellia", "Alnylam", "Onpattro", "BioMarin", "Vimizim",
            "Exelixis", "Cabometyx", "Ionis", "Spinraza", "Incyte", "Jakafi", "Seagen",
            "BeiGene", "UCB", "Cimzia", "Grifols", "CSL", "Behring", "Lonza", "Catalent",
            "WuXi", "AppTec", "Charles", "River", "Laboratories",

            // --- ÖLBRANCHE & ENERGIE (OIL & GAS) ---
            "Shell", "Aramco", "BP", "TotalEnergies", "Total", "Equinor", "Petrobras", "Eni",
            "Repsol", "OMV", "CNOOC", "Sinopec", "PetroChina", "Kinder", "Morgan",
            "Enterprise", "Products", "Partners", "Williams", "Companies", "ONEOK", "TC",
            "Enbridge", "Cheniere", "Diamondback", "Permian", "Coterra", "EQT", "Chesapeake",
            "Antero", "Murphy", "Oil", "Crescent", "Civitas",

            // --- SOFTWARE & CLOUD SERVICES ---
            "Twilio", "SendGrid", "Asana", "Monday", "Box", "Dropbox", "DocuSign",
            "Zoom", "RingCentral", "PagerDuty", "GitLab", "GitHub", "Docker", "Kubernetes",
            "HashiCorp", "Terraform", "Vault", "Confluent", "Kafka", "Elastic", "Kibana",
            "Logstash", "Dynatrace", "Sumo", "Logic", "Samsara", "Procore", "Smartsheet",
            "Jamf", "Pegasystems", "Appian", "Guidewire", "Tyler", "Blackline", "Alteryx",
            "UiPath", "Automation", "Anywhere"
        };




        public static readonly HashSet<string> PersonalizedItemTypes = new(StringComparer.Ordinal)
        {
            "Task", "Note", "Appointment", "Reminder"
        };


        public static bool IsPersonalizedItemType(this IItemType itemType)
        {
            if (string.IsNullOrEmpty(itemType.Name))
            {
                return false;
            }

            return PersonalizedItemTypes.Contains(itemType.Name);
        }

    }
}
