using System;
using System.Collections.Generic;
using UnityEngine.Device;

namespace Core.Scripts.RetenoSdk.Common
{
    /// <summary>
    /// The dictionary helper class
    /// </summary>
    public static class DictionaryHelper
    {
        /// <summary>
        /// The dictionary
        /// </summary>
        private static readonly Dictionary<string, string> TimeZonesDictionary = new();

        /// <summary>
        /// The dictionary
        /// </summary>
        private static readonly Dictionary<string, string> LanguageCodesDictionary = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryHelper"/> class
        /// </summary>
        static DictionaryHelper()
        {
            InitializeTimeZonesDictionary();
            InitializeLanguageCodesDictionary();
        }

        /// <summary>
        /// Gets the olson time zone
        /// </summary>
        /// <returns>The string</returns>
        public static string GetOlsonTimeZone()
        {
            return TimeZonesDictionary.TryGetValue(TimeZoneInfo.Local.DisplayName, out var olsonTimeZone) ? olsonTimeZone : "Europe/Kyiv";
        }

        /// <summary>
        /// Gets the language code
        /// </summary>
        /// <returns>The string</returns>
        public static string GetLanguageCode()
        {
            return LanguageCodesDictionary.TryGetValue(Application.systemLanguage.ToString(), out var languageCode) ? languageCode : "uk";
        }

        /// <summary>
        /// Initializes the time zones dictionary
        /// </summary>
        private static void InitializeTimeZonesDictionary()
        {
            TimeZonesDictionary.Add("(UTC-12:00) International Date Line West", "Etc/GMT+12");
            TimeZonesDictionary.Add("(UTC-11:00) Coordinated Universal Time-11", "Etc/GMT+11");
            TimeZonesDictionary.Add("(UTC-10:00) Aleutian Islands", "America/Adak");
            TimeZonesDictionary.Add("(UTC-10:00) Hawaii", "Pacific/Honolulu");
            TimeZonesDictionary.Add("(UTC-09:30) Marquesas Islands", "Pacific/Marquesas");
            TimeZonesDictionary.Add("(UTC-09:00) Alaska", "America/Anchorage");
            TimeZonesDictionary.Add("(UTC-09:00) Coordinated Universal Time-09", "Etc/GMT+9");
            TimeZonesDictionary.Add("(UTC-08:00) Baja California", "America/Tijuana");
            TimeZonesDictionary.Add("(UTC-08:00) Coordinated Universal Time-08", "Etc/GMT+8");
            TimeZonesDictionary.Add("(UTC-08:00) Pacific Time (US & Canada)", "America/Los_Angeles");
            TimeZonesDictionary.Add("(UTC-07:00) Arizona", "America/Phoenix");
            TimeZonesDictionary.Add("(UTC-07:00) La Paz, Mazatlan", "America/Chihuahua");
            TimeZonesDictionary.Add("(UTC-07:00) Mountain Time (US & Canada)", "America/Denver");
            TimeZonesDictionary.Add("(UTC-07:00) Yukon", "America/Whitehorse");
            TimeZonesDictionary.Add("(UTC-06:00) Central America", "America/Guatemala");
            TimeZonesDictionary.Add("(UTC-06:00) Central Time (US & Canada)", "America/Chicago");
            TimeZonesDictionary.Add("(UTC-06:00) Easter Island", "Pacific/Easter");
            TimeZonesDictionary.Add("(UTC-06:00) Guadalajara, Mexico City, Monterrey", "America/Mexico_City");
            TimeZonesDictionary.Add("(UTC-06:00) Saskatchewan", "America/Regina");
            TimeZonesDictionary.Add("(UTC-05:00) Bogota, Lima, Quito, Rio Branco", "America/Bogota");
            TimeZonesDictionary.Add("(UTC-05:00) Chetumal", "America/Cancun");
            TimeZonesDictionary.Add("(UTC-05:00) Eastern Time (US & Canada)", "America/New_York");
            TimeZonesDictionary.Add("(UTC-05:00) Haiti", "America/Port-au-Prince");
            TimeZonesDictionary.Add("(UTC-05:00) Havana", "America/Havana");
            TimeZonesDictionary.Add("(UTC-05:00) Indiana (East)", "America/Indiana/Indianapolis");
            TimeZonesDictionary.Add("(UTC-05:00) Turks and Caicos", "America/Grand_Turk");
            TimeZonesDictionary.Add("(UTC-04:00) Asuncion", "America/Asuncion");
            TimeZonesDictionary.Add("(UTC-04:00) Atlantic Time (Canada)", "America/Halifax");
            TimeZonesDictionary.Add("(UTC-04:00) Caracas", "America/Caracas");
            TimeZonesDictionary.Add("(UTC-04:00) Cuiaba", "America/Cuiaba");
            TimeZonesDictionary.Add("(UTC-04:00) Georgetown, La Paz, Manaus, San Juan", "America/La_Paz");
            TimeZonesDictionary.Add("(UTC-04:00) Santiago", "America/Santiago");
            TimeZonesDictionary.Add("(UTC-03:30) Newfoundland", "America/St_Johns");
            TimeZonesDictionary.Add("(UTC-03:00) Araguaina", "America/Araguaina");
            TimeZonesDictionary.Add("(UTC-03:00) Brasilia", "America/Sao_Paulo");
            TimeZonesDictionary.Add("(UTC-03:00) Cayenne, Fortaleza", "America/Fortaleza");
            TimeZonesDictionary.Add("(UTC-03:00) City of Buenos Aires", "America/Argentina/Buenos_Aires");
            TimeZonesDictionary.Add("(UTC-03:00) Greenland", "America/Godthab");
            TimeZonesDictionary.Add("(UTC-03:00) Montevideo", "America/Montevideo");
            TimeZonesDictionary.Add("(UTC-03:00) Punta Arenas", "America/Punta_Arenas");
            TimeZonesDictionary.Add("(UTC-03:00) Saint Pierre and Miquelon", "America/Miquelon");
            TimeZonesDictionary.Add("(UTC-03:00) Salvador", "America/Bahia");
            TimeZonesDictionary.Add("(UTC-02:00) Coordinated Universal Time-02", "Etc/GMT+2");
            TimeZonesDictionary.Add("(UTC-02:00) Mid-Atlantic - Old", "Etc/GMT+2");
            TimeZonesDictionary.Add("(UTC-01:00) Azores", "Atlantic/Azores");
            TimeZonesDictionary.Add("(UTC-01:00) Cabo Verde Is.", "Atlantic/Cape_Verde");
            TimeZonesDictionary.Add("(UTC) Coordinated Universal Time", "Etc/UTC");
            TimeZonesDictionary.Add("(UTC+00:00) Dublin, Edinburgh, Lisbon, London", "Europe/London");
            TimeZonesDictionary.Add("(UTC+00:00) Monrovia, Reykjavik", "Atlantic/Reykjavik");
            TimeZonesDictionary.Add("(UTC+00:00) Sao Tome", "Africa/Sao_Tome");
            TimeZonesDictionary.Add("(UTC+01:00) Casablanca", "Africa/Casablanca");
            TimeZonesDictionary.Add("(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna", "Europe/Berlin");
            TimeZonesDictionary.Add("(UTC+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague", "Europe/Belgrade");
            TimeZonesDictionary.Add("(UTC+01:00) Brussels, Copenhagen, Madrid, Paris", "Europe/Paris");
            TimeZonesDictionary.Add("(UTC+01:00) Sarajevo, Skopje, Warsaw, Zagreb", "Europe/Warsaw");
            TimeZonesDictionary.Add("(UTC+01:00) West Central Africa", "Africa/Lagos");
            TimeZonesDictionary.Add("(UTC+02:00) Athens, Bucharest", "Europe/Athens");
            TimeZonesDictionary.Add("(UTC+02:00) Beirut", "Asia/Beirut");
            TimeZonesDictionary.Add("(UTC+02:00) Cairo", "Africa/Cairo");
            TimeZonesDictionary.Add("(UTC+02:00) Chisinau", "Europe/Chisinau");
            TimeZonesDictionary.Add("(UTC+02:00) Damascus", "Asia/Damascus");
            TimeZonesDictionary.Add("(UTC+02:00) Gaza, Hebron", "Asia/Gaza");
            TimeZonesDictionary.Add("(UTC+02:00) Harare, Pretoria", "Africa/Harare");
            TimeZonesDictionary.Add("(UTC+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius", "Europe/Kyiv");
            TimeZonesDictionary.Add("(UTC+02:00) Jerusalem", "Asia/Jerusalem");
            TimeZonesDictionary.Add("(UTC+02:00) Juba", "Africa/Juba");
            TimeZonesDictionary.Add("(UTC+02:00) Kaliningrad", "Europe/Kaliningrad");
            TimeZonesDictionary.Add("(UTC+02:00) Khartoum", "Africa/Khartoum");
            TimeZonesDictionary.Add("(UTC+02:00) Tripoli", "Africa/Tripoli");
            TimeZonesDictionary.Add("(UTC+02:00) Windhoek", "Africa/Windhoek");
            TimeZonesDictionary.Add("(UTC+03:00) Amman", "Asia/Amman");
            TimeZonesDictionary.Add("(UTC+03:00) Baghdad", "Asia/Baghdad");
            TimeZonesDictionary.Add("(UTC+03:00) Istanbul", "Europe/Istanbul");
            TimeZonesDictionary.Add("(UTC+03:00) Kuwait, Riyadh", "Asia/Riyadh");
            TimeZonesDictionary.Add("(UTC+03:00) Minsk", "Europe/Minsk");
            TimeZonesDictionary.Add("(UTC+03:00) Moscow, St. Petersburg", "Europe/Moscow");
            TimeZonesDictionary.Add("(UTC+03:00) Nairobi", "Africa/Nairobi");
            TimeZonesDictionary.Add("(UTC+03:00) Volgograd", "Europe/Volgograd");
            TimeZonesDictionary.Add("(UTC+03:30) Tehran", "Asia/Tehran");
            TimeZonesDictionary.Add("(UTC+04:00) Abu Dhabi, Muscat", "Asia/Dubai");
            TimeZonesDictionary.Add("(UTC+04:00) Astrakhan, Ulyanovsk", "Europe/Astrakhan");
            TimeZonesDictionary.Add("(UTC+04:00) Baku", "Asia/Baku");
            TimeZonesDictionary.Add("(UTC+04:00) Izhevsk, Samara", "Europe/Samara");
            TimeZonesDictionary.Add("(UTC+04:00) Port Louis", "Indian/Mauritius");
            TimeZonesDictionary.Add("(UTC+04:00) Saratov", "Europe/Saratov");
            TimeZonesDictionary.Add("(UTC+04:00) Tbilisi", "Asia/Tbilisi");
            TimeZonesDictionary.Add("(UTC+04:00) Yerevan", "Asia/Yerevan");
            TimeZonesDictionary.Add("(UTC+04:30) Kabul", "Asia/Kabul");
            TimeZonesDictionary.Add("(UTC+05:00) Ashgabat, Tashkent", "Asia/Tashkent");
            TimeZonesDictionary.Add("(UTC+05:00) Ekaterinburg", "Asia/Yekaterinburg");
            TimeZonesDictionary.Add("(UTC+05:00) Islamabad, Karachi", "Asia/Karachi");
            TimeZonesDictionary.Add("(UTC+05:00) Qyzylorda", "Asia/Qyzylorda");
            TimeZonesDictionary.Add("(UTC+05:30) Chennai, Kolkata, Mumbai, New Delhi", "Asia/Kolkata");
            TimeZonesDictionary.Add("(UTC+05:30) Sri Jayawardenepura", "Asia/Colombo");
            TimeZonesDictionary.Add("(UTC+05:45) Kathmandu", "Asia/Kathmandu");
            TimeZonesDictionary.Add("(UTC+06:00) Astana", "Asia/Almaty");
            TimeZonesDictionary.Add("(UTC+06:00) Dhaka", "Asia/Dhaka");
            TimeZonesDictionary.Add("(UTC+06:00) Omsk", "Asia/Omsk");
            TimeZonesDictionary.Add("(UTC+06:30) Yangon (Rangoon)", "Asia/Yangon");
            TimeZonesDictionary.Add("(UTC+07:00) Bangkok, Hanoi, Jakarta", "Asia/Bangkok");
            TimeZonesDictionary.Add("(UTC+07:00) Barnaul, Gorno-Altaysk", "Asia/Barnaul");
            TimeZonesDictionary.Add("(UTC+07:00) Hovd", "Asia/Hovd");
            TimeZonesDictionary.Add("(UTC+07:00) Krasnoyarsk", "Asia/Krasnoyarsk");
            TimeZonesDictionary.Add("(UTC+07:00) Novosibirsk", "Asia/Novosibirsk");
            TimeZonesDictionary.Add("(UTC+07:00) Tomsk", "Asia/Tomsk");
            TimeZonesDictionary.Add("(UTC+08:00) Beijing, Chongqing, Hong Kong, Urumqi", "Asia/Shanghai");
            TimeZonesDictionary.Add("(UTC+08:00) Irkutsk", "Asia/Irkutsk");
            TimeZonesDictionary.Add("(UTC+08:00) Kuala Lumpur, Singapore", "Asia/Kuala_Lumpur");
            TimeZonesDictionary.Add("(UTC+08:00) Perth", "Australia/Perth");
            TimeZonesDictionary.Add("(UTC+08:00) Taipei", "Asia/Taipei");
            TimeZonesDictionary.Add("(UTC+08:00) Ulaanbaatar", "Asia/Ulaanbaatar");
            TimeZonesDictionary.Add("(UTC+08:45) Eucla", "Australia/Eucla");
            TimeZonesDictionary.Add("(UTC+09:00) Chita", "Asia/Chita");
            TimeZonesDictionary.Add("(UTC+09:00) Osaka, Sapporo, Tokyo", "Asia/Tokyo");
            TimeZonesDictionary.Add("(UTC+09:00) Pyongyang", "Asia/Pyongyang");
            TimeZonesDictionary.Add("(UTC+09:00) Seoul", "Asia/Seoul");
            TimeZonesDictionary.Add("(UTC+09:00) Yakutsk", "Asia/Yakutsk");
            TimeZonesDictionary.Add("(UTC+09:30) Adelaide", "Australia/Adelaide");
            TimeZonesDictionary.Add("(UTC+09:30) Darwin", "Australia/Darwin");
            TimeZonesDictionary.Add("(UTC+10:00) Brisbane", "Australia/Brisbane");
            TimeZonesDictionary.Add("(UTC+10:00) Canberra, Melbourne, Sydney", "Australia/Sydney");
            TimeZonesDictionary.Add("(UTC+10:00) Guam, Port Moresby", "Pacific/Guam");
            TimeZonesDictionary.Add("(UTC+10:00) Hobart", "Australia/Hobart");
            TimeZonesDictionary.Add("(UTC+10:00) Vladivostok", "Asia/Vladivostok");
            TimeZonesDictionary.Add("(UTC+10:30) Lord Howe Island", "Australia/Lord_Howe");
            TimeZonesDictionary.Add("(UTC+11:00) Bougainville Island", "Pacific/Bougainville");
            TimeZonesDictionary.Add("(UTC+11:00) Chokurdakh", "Asia/Srednekolymsk");
            TimeZonesDictionary.Add("(UTC+11:00) Magadan", "Asia/Magadan");
            TimeZonesDictionary.Add("(UTC+11:00) Norfolk Island", "Pacific/Norfolk");
            TimeZonesDictionary.Add("(UTC+11:00) Sakhalin", "Asia/Sakhalin");
            TimeZonesDictionary.Add("(UTC+11:00) Solomon Is., New Caledonia", "Pacific/Guadalcanal");
            TimeZonesDictionary.Add("(UTC+12:00) Anadyr, Petropavlovsk-Kamchatsky", "Asia/Anadyr");
            TimeZonesDictionary.Add("(UTC+12:00) Auckland, Wellington", "Pacific/Auckland");
            TimeZonesDictionary.Add("(UTC+12:00) Coordinated Universal Time+12", "Etc/GMT-12");
            TimeZonesDictionary.Add("(UTC+12:00) Fiji", "Pacific/Fiji");
            TimeZonesDictionary.Add("(UTC+12:00) Petropavlovsk-Kamchatsky - Old", "Asia/Kamchatka");
            TimeZonesDictionary.Add("(UTC+12:45) Chatham Islands", "Pacific/Chatham");
            TimeZonesDictionary.Add("(UTC+13:00) Coordinated Universal Time+13", "Etc/GMT-13");
            TimeZonesDictionary.Add("(UTC+13:00) Nuku'alofa", "Pacific/Tongatapu");
            TimeZonesDictionary.Add("(UTC+13:00) Samoa", "Pacific/Apia");
            TimeZonesDictionary.Add("(UTC+14:00) Kiritimati Island", "Pacific/Kiritimati");
        }

        /// <summary>
        /// Initializes the language codes dictionary
        /// </summary>
        private static void InitializeLanguageCodesDictionary()
        {
            LanguageCodesDictionary.Add("Afrikaans", "af");
            LanguageCodesDictionary.Add("Arabic", "ar");
            LanguageCodesDictionary.Add("Basque", "eu");
            LanguageCodesDictionary.Add("Belarusian", "be");
            LanguageCodesDictionary.Add("Bulgarian", "bg");
            LanguageCodesDictionary.Add("Catalan", "ca");
            LanguageCodesDictionary.Add("Chinese", "zh");
            LanguageCodesDictionary.Add("Czech", "cs");
            LanguageCodesDictionary.Add("Danish", "da");
            LanguageCodesDictionary.Add("Dutch", "nl");
            LanguageCodesDictionary.Add("English", "en");
            LanguageCodesDictionary.Add("Estonian", "et");
            LanguageCodesDictionary.Add("Faroese", "fo");
            LanguageCodesDictionary.Add("Finnish", "fi");
            LanguageCodesDictionary.Add("French", "fr");
            LanguageCodesDictionary.Add("German", "de");
            LanguageCodesDictionary.Add("Greek", "el");
            LanguageCodesDictionary.Add("Hebrew", "he");
            LanguageCodesDictionary.Add("Hungarian", "hu");
            LanguageCodesDictionary.Add("Icelandic", "is");
            LanguageCodesDictionary.Add("Indonesian", "id");
            LanguageCodesDictionary.Add("Italian", "it");
            LanguageCodesDictionary.Add("Japanese", "ja");
            LanguageCodesDictionary.Add("Korean", "ko");
            LanguageCodesDictionary.Add("Latvian", "lv");
            LanguageCodesDictionary.Add("Lithuanian", "lt");
            LanguageCodesDictionary.Add("Norwegian", "no");
            LanguageCodesDictionary.Add("Polish", "pl");
            LanguageCodesDictionary.Add("Portuguese", "pt");
            LanguageCodesDictionary.Add("Romanian", "ro");
            LanguageCodesDictionary.Add("Russian", "ru");
            LanguageCodesDictionary.Add("SerboCroatian", "sh");
            LanguageCodesDictionary.Add("Slovak", "sk");
            LanguageCodesDictionary.Add("Slovenian", "sl");
            LanguageCodesDictionary.Add("Spanish", "es");
            LanguageCodesDictionary.Add("Swedish", "sv");
            LanguageCodesDictionary.Add("Thai", "th");
            LanguageCodesDictionary.Add("Turkish", "tr");
            LanguageCodesDictionary.Add("Ukrainian", "uk");
            LanguageCodesDictionary.Add("Vietnamese", "vi");
            LanguageCodesDictionary.Add("ChineseSimplified", "zh");
            LanguageCodesDictionary.Add("ChineseTraditional", "zh");
        }
    }
}