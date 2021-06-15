using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.DTOs
{
    public class ErrorDto
    {
        // Errors den yeni bir liste oluşturalım ki içerisini doldurabilelim.
        // Ben değişkeni tanımlaşım 17.satırda Errors diye ama buna eklmeye çalışıyor ama
        // new ile 17.satırdan List<String> bir nesne örneği oluşturmamışım dolayısıyla ekleyemiyor.
        // errorDtoya biz nesne örneği aldığımız ilk anda constructurunda new List<string> diyerek
        // listi oluştuuryoru.
        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public List<String>  Errors {get;set;}
        public int Status { get; set; }

    }
}
