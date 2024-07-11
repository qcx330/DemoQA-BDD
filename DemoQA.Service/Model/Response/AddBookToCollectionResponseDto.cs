using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DemoQA.Service.Model.Response
{
    public class AddBookToCollectionResponseDto
    {
        [JsonProperty("books")]
        public List<BookResponseDto> Books { get; set; }
    }
}