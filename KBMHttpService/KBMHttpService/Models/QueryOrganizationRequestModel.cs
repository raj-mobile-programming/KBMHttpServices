﻿namespace KBMHttpService.Models
{
    public class QueryOrganizationRequestModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string orderBy { get; set; }
        public string direction { get; set; }
        public string queryString { get; set; }
    }
}
