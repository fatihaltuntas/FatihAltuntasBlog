#pragma checksum "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f55b48e895d334bbb71ea389d68272a33f2f123"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_Category_Index), @"mvc.1.0.view", @"/Areas/Admin/Views/Category/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f55b48e895d334bbb71ea389d68272a33f2f123", @"/Areas/Admin/Views/Category/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a9af4978b9c2bfca24ef48e96efe5f8573634464", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    public class Areas_Admin_Views_Category_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FatihAltuntasBlog.Entities.Dtos.CategoryListDto>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            WriteLiteral("\n");
#nullable restore
#line 4 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
   
    Layout = "_Layout";
    ViewBag.Title = "Kategoriler Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""card mb-4"">
    <div class=""card-header"">
        <i class=""fas fa-table mr-1""></i>
        Kategoriler
    </div>
    <div class=""card-body"">
        <div class=""table-responsive"">
            <table class=""table table-bordered"" id=""categoriesTable"" width=""100%"" cellspacing=""0"">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Adı</th>
                        <th>Açıklaması</th>
                        <th>Aktif Mi?</th>
                        <th>Silinmiş Mi?</th>
                        <th>Not</th>
                        <th>Oluşturulma Tarihi</th>
                        <th>Oluşturan Kullanıcı Adı</th>
                        <th>Son Düzenlenme Tarihi</th>
                        <th>Son Düzenleyen Kullanıcı Adı</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 32 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                     foreach (var category in Model.Categories)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <td>");
#nullable restore
#line 35 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 36 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 37 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 38 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.IsActive);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 39 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.IsDeleted);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 40 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.Note);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 41 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.CreatedDate.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 42 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.CreatedByName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 43 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.ModifiedDate.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                            <td>");
#nullable restore
#line 44 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                           Write(category.ModifiedByName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        </tr>\n");
#nullable restore
#line 46 "C:\Users\Kodiks\Documents\FatihAltuntasBlog\FatihAltuntasBlog\FatihAltuntasBlog.Mvc\Areas\Admin\Views\Category\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\n            </table>\n        </div>\n    </div>\n</div>\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            $('#categoriesTable').DataTable({
                dom:
                    ""<'row'<'col-sm-3'l><'col-sm-6 text-center'B><'col-sm-3'f>>"" +
                    ""<'row'<'col-sm-12'tr>>"" +
                    ""<'row'<'col-sm-5'i><'col-sm-7'p>>"",
                buttons: [
                    {
                        text: 'Ekle',
                        className:'btn btn-success'
                        action: function (e, dt, node, config) {
                            dt.ajax.reload();
                        }
                    },
                    {
                        text: 'Yenile',
                        className: 'btn btn-warning'
                        action: function (e, dt, node, config) {
                            dt.ajax.reload();
                        }
                    }
                ],
                {
                    ""emptyTable"": ""Tabloda herhangi bir veri mevcut değil"",
          ");
                WriteLiteral(@"          ""info"": ""_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor"",
                    ""infoEmpty"": ""Kayıt yok"",
                    ""infoFiltered"": ""(_MAX_ kayıt içerisinden bulunan)"",
                    ""infoThousands"": ""."",
                    ""lengthMenu"": ""Sayfada _MENU_ kayıt göster"",
                    ""loadingRecords"": ""Yükleniyor..."",
                    ""processing"": ""İşleniyor..."",
                    ""search"": ""Ara:"",
                    ""zeroRecords"": ""Eşleşen kayıt bulunamadı"",
                    ""paginate"": {
                        ""first"": ""İlk"",
                        ""last"": ""Son"",
                        ""next"": ""Sonraki"",
                        ""previous"": ""Önceki""
                    },
                    ""aria"": {
                        ""sortAscending"": "": artan sütun sıralamasını aktifleştir"",
                        ""sortDescending"": "": azalan sütun sıralamasını aktifleştir""
                    },
                    ""select"": {
         ");
                WriteLiteral(@"               ""rows"": {
                            ""_"": ""%d kayıt seçildi"",
                            ""1"": ""1 kayıt seçildi""
                        },
                        ""cells"": {
                            ""1"": ""1 hücre seçildi"",
                            ""_"": ""%d hücre seçildi""
                        },
                        ""columns"": {
                            ""1"": ""1 sütun seçildi"",
                            ""_"": ""%d sütun seçildi""
                        }
                    },
                    ""autoFill"": {
                        ""cancel"": ""İptal"",
                        ""fillHorizontal"": ""Hücreleri yatay olarak doldur"",
                        ""fillVertical"": ""Hücreleri dikey olarak doldur"",
                        ""fill"": ""Bütün hücreleri <i>%d<\/i> ile doldur""
                    },
                    ""buttons"": {
                        ""collection"": ""Koleksiyon <span class=\""ui-button-icon-primary ui-icon ui-icon-triangle-1-s\""><\/span>"",
         ");
                WriteLiteral(@"               ""colvis"": ""Sütun görünürlüğü"",
                        ""colvisRestore"": ""Görünürlüğü eski haline getir"",
                        ""copySuccess"": {
                            ""1"": ""1 satır panoya kopyalandı"",
                            ""_"": ""%ds satır panoya kopyalandı""
                        },
                        ""copyTitle"": ""Panoya kopyala"",
                        ""csv"": ""CSV"",
                        ""excel"": ""Excel"",
                        ""pageLength"": {
                            ""-1"": ""Bütün satırları göster"",
                            ""_"": ""%d satır göster""
                        },
                        ""pdf"": ""PDF"",
                        ""print"": ""Yazdır"",
                        ""copy"": ""Kopyala"",
                        ""copyKeys"": ""Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın.""
                    },
                    ""searchBuilder"": {
                     ");
                WriteLiteral(@"   ""add"": ""Koşul Ekle"",
                        ""button"": {
                            ""0"": ""Arama Oluşturucu"",
                            ""_"": ""Arama Oluşturucu (%d)""
                        },
                        ""condition"": ""Koşul"",
                        ""conditions"": {
                            ""date"": {
                                ""after"": ""Sonra"",
                                ""before"": ""Önce"",
                                ""between"": ""Arasında"",
                                ""empty"": ""Boş"",
                                ""equals"": ""Eşittir"",
                                ""not"": ""Değildir"",
                                ""notBetween"": ""Dışında"",
                                ""notEmpty"": ""Dolu""
                            },
                            ""number"": {
                                ""between"": ""Arasında"",
                                ""empty"": ""Boş"",
                                ""equals"": ""Eşittir"",
                                ""gt"": ""B");
                WriteLiteral(@"üyüktür"",
                                ""gte"": ""Büyük eşittir"",
                                ""lt"": ""Küçüktür"",
                                ""lte"": ""Küçük eşittir"",
                                ""not"": ""Değildir"",
                                ""notBetween"": ""Dışında"",
                                ""notEmpty"": ""Dolu""
                            },
                            ""string"": {
                                ""contains"": ""İçerir"",
                                ""empty"": ""Boş"",
                                ""endsWith"": ""İle biter"",
                                ""equals"": ""Eşittir"",
                                ""not"": ""Değildir"",
                                ""notEmpty"": ""Dolu"",
                                ""startsWith"": ""İle başlar""
                            },
                            ""array"": {
                                ""contains"": ""İçerir"",
                                ""empty"": ""Boş"",
                                ""equals"": ""Eşittir"",
  ");
                WriteLiteral(@"                              ""not"": ""Değildir"",
                                ""notEmpty"": ""Dolu"",
                                ""without"": ""Hariç""
                            }
                        },
                        ""data"": ""Veri"",
                        ""deleteTitle"": ""Filtreleme kuralını silin"",
                        ""leftTitle"": ""Kriteri dışarı çıkart"",
                        ""logicAnd"": ""ve"",
                        ""logicOr"": ""veya"",
                        ""rightTitle"": ""Kriteri içeri al"",
                        ""title"": {
                            ""0"": ""Arama Oluşturucu"",
                            ""_"": ""Arama Oluşturucu (%d)""
                        },
                        ""value"": ""Değer"",
                        ""clearAll"": ""Filtreleri Temizle""
                    },
                    ""searchPanes"": {
                        ""clearMessage"": ""Hepsini Temizle"",
                        ""collapse"": {
                            ""0"": ""Arama Bölmesi"",
  ");
                WriteLiteral(@"                          ""_"": ""Arama Bölmesi (%d)""
                        },
                        ""count"": ""{total}"",
                        ""countFiltered"": ""{shown}\/{total}"",
                        ""emptyPanes"": ""Arama Bölmesi yok"",
                        ""loadMessage"": ""Arama Bölmeleri yükleniyor ..."",
                        ""title"": ""Etkin filtreler - %d""
                    },
                    ""thousands"": ""."",
                    ""datetime"": {
                        ""amPm"": [
                            ""öö"",
                            ""ös""
                        ],
                        ""hours"": ""Saat"",
                        ""minutes"": ""Dakika"",
                        ""next"": ""Sonraki"",
                        ""previous"": ""Önceki"",
                        ""seconds"": ""Saniye"",
                        ""unknown"": ""Bilinmeyen"",
                        ""weekdays"": {
                            ""6"": ""Paz"",
                            ""5"": ""Cmt"",
                   ");
                WriteLiteral(@"         ""4"": ""Cum"",
                            ""3"": ""Per"",
                            ""2"": ""Çar"",
                            ""1"": ""Sal"",
                            ""0"": ""Pzt""
                        },
                        ""months"": {
                            ""9"": ""Ekim"",
                            ""8"": ""Eylül"",
                            ""7"": ""Ağustos"",
                            ""6"": ""Temmuz"",
                            ""5"": ""Haziran"",
                            ""4"": ""Mayıs"",
                            ""3"": ""Nisan"",
                            ""2"": ""Mart"",
                            ""11"": ""Aralık"",
                            ""10"": ""Kasım"",
                            ""1"": ""Şubat"",
                            ""0"": ""Ocak""
                        }
                    },
                    ""decimal"": "","",
                    ""editor"": {
                        ""close"": ""Kapat"",
                        ""create"": {
                            ""button"": ""Yeni"",
      ");
                WriteLiteral(@"                      ""submit"": ""Kaydet"",
                            ""title"": ""Yeni kayıt oluştur""
                        },
                        ""edit"": {
                            ""button"": ""Düzenle"",
                            ""submit"": ""Güncelle"",
                            ""title"": ""Kaydı düzenle""
                        },
                        ""error"": {
                            ""system"": ""Bir sistem hatası oluştu (Ayrıntılı bilgi)""
                        },
                        ""multi"": {
                            ""info"": ""Seçili kayıtlar bu alanda farklı değerler içeriyor. Seçili kayıtların hepsinde bu alana aynı değeri atamak için buraya tıklayın; aksi halde her kayıt bu alanda kendi değerini koruyacak."",
                            ""noMulti"": ""Bu alan bir grup olarak değil ancak tekil olarak düzenlenebilir."",
                            ""restore"": ""Değişiklikleri geri al"",
                            ""title"": ""Çoklu değer""
                        },
            ");
                WriteLiteral(@"            ""remove"": {
                            ""button"": ""Sil"",
                            ""confirm"": {
                                ""_"": ""%d adet kaydı silmek istediğinize emin misiniz?"",
                                ""1"": ""Bu kaydı silmek istediğinizden emin misiniz?""
                            },
                            ""submit"": ""Sil"",
                            ""title"": ""Kayıtları sil""
                        }
                    }
                }
            });
        });
    </script>
");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FatihAltuntasBlog.Entities.Dtos.CategoryListDto> Html { get; private set; }
    }
}
#pragma warning restore 1591