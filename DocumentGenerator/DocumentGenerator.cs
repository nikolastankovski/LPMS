using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ClassLibrary1;

public class TableHeader()
{
    public string NameMK { get; set; }
    public string NameEn { get; set; }
}

public class Insurer()
{
    public string OrderNumber { get; set; }
    public string Name { get; set; }
    public string EMBG { get; set; }
    public string PassportNo { get; set; }
    public string SportRisk { get; set; }
    public string Covid19 { get; set; }
    public string ProfessionalDriver { get; set; }
    public string StudentAbroad { get; set; }
}

public static class DocumentGenerator
{
    public static void Generate()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\", nameof(DocumentGenerator),
            "generated-docs",
            "helloworld.pdf");

        var imagesFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\",
            nameof(DocumentGenerator), "images"));
        var primaryColor = "#3ca082";
        QuestPDF.Settings.License = LicenseType.Community;
        
        static IContainer Block(IContainer container)
        {
            return container
                .BorderBottom(1)
                .Background(Colors.Grey.Lighten5)
                .ShowOnce()
                .Padding(3)
                .MinWidth(10)
                .MinHeight(10)
                .AlignMiddle();
        }
                                
        static IContainer Entry(IContainer container)
        {
            return container
                .BorderBottom(1)
                .PaddingVertical(2)
                .PaddingHorizontal(3)
                .ShowOnce()
                .AlignMiddle();
        }
        
       Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.MarginHorizontal(5, Unit.Millimetre);
                    page.MarginVertical(10, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(8));

                    page.Header()
                        .Grid(grid =>
                        {
                            grid.HorizontalSpacing(15);
                            grid.AlignCenter();
                            grid.Columns(4);

                            grid.Item(1)
                                .AlignCenter()
                                .Height(15, Unit.Millimetre)
                                .Image(Path.Combine(imagesFolder, "sava-logo.png")).FitArea();
                            grid.Item(1)
                                .AlignCenter()
                                .PaddingTop(3, Unit.Millimetre)
                                .Column(col =>
                                {
                                    col.Item().Text("Ул. Загребска бр.28А");
                                    col.Item().Text("1000 Скопје");
                                    col.Item().Text("Р. Македонија");
                                });
                            grid.Item(1)
                                .AlignCenter()
                                .Height(15, Unit.Millimetre)
                                .Image(Path.Combine(imagesFolder, "24-7.png")).FitArea();
                            grid.Item(1)
                                .AlignCenter()
                                .PaddingTop(3, Unit.Millimetre)
                                .Column(col =>
                                {
                                    col.Item().Text("тел./tel.: +389 2 5101 525");
                                    col.Item().Text("24 часа сервис / 24 hours service");
                                });
                        });

                    page.Content()
                        .PaddingTop(30)
                        .Column(mainCol =>
                        {
                            mainCol.Item()
                                .Row(row =>
                                {

                                    row.Spacing(20);

                                    row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                                    {
                                        col.Item().Text("ПОЛИСА ЗА ПАТНИЧКО ОСИГУРУВАЊЕ").Bold().AlignCenter();
                                        col.Item().Text("TRAVELLERS INSURANCE").Bold().AlignCenter();
                                        col.Item().Text("SAVA INSURANCE").Bold().AlignCenter();
                                    });
                                    row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                                    {
                                        col.Item().Text("Број на полиса / Policy No.").AlignCenter();
                                        col.Item().Text("PZO 080224").Bold().AlignCenter();
                                    });
                                });

                            mainCol.Item().PaddingVertical(5).LineHorizontal(1).LineColor(primaryColor);

                            mainCol.Item()
                                .Row(row =>
                                {
                                    row.Spacing(20);

                                    row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                                    {
                                        col.Item().Text("Референтен број / Reference No.").AlignCenter();
                                        col.Item().Text("114-210224").Bold().AlignCenter();
                                    });
                                    row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                                    {
                                        col.Item().Text("Ознака на трансакцијата / Transaction ID").AlignCenter();
                                        col.Item().Text("C-372a56d966d322118fde").Bold().AlignCenter();
                                    });
                                });

                            mainCol.Item().PaddingVertical(5).LineHorizontal(1).LineColor(primaryColor);

                            mainCol.Item()
                                .Grid(grid =>
                                {
                                    grid.Columns(2);

                                    grid.Item(1).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Договорувач/Contractor:");
                                        row.AutoItem().Text("Никола Станковски 1309998450025").Bold();
                                    });
                                    grid.Item(1).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Тип/Type:");
                                        row.AutoItem().Text("Индивидуално").Bold();
                                    });

                                    grid.Item(1).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Осигуреник/Insured:");
                                        row.AutoItem().Text("На списокот / On the list").Bold();
                                    });
                                    grid.Item(1).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Лица/Persons:");
                                        row.AutoItem().Text("3").Bold();
                                    });

                                    grid.Item(1).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Адреса/Address:");
                                        row.AutoItem().Text("П. Филиповски Гарката бр.4/1-1, 1000 Skopje").Bold();
                                    });
                                    grid.Item(1).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Вид на покритие/Type of coverage:");
                                        row.AutoItem().Text("PLATINUM").Bold();
                                    });

                                    grid.Item(1).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Започнува на/Valid from:");
                                        row.AutoItem().Text("23.07.2022").Bold();
                                    });
                                    grid.Item(1).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5)
                                            .Text("Важи за сите земји / Valid for all countries");
                                    });

                                    grid.Item(2).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Завршува на/Valid until:");
                                        row.AutoItem().Text("31.07.2022").Bold();
                                    });

                                    grid.Item(2).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Период на покритие/Insured period:");
                                        row.AutoItem().Text("9").Bold();
                                    });

                                    grid.Item(2).Row(row =>
                                    {
                                        row.AutoItem().PaddingRight(5).Text("Франшиза/Deductible:");
                                        row.AutoItem().Text("Без Франшиза / No deductible").Bold();
                                    });
                                });

                            mainCol.Item().PaddingVertical(5).LineHorizontal(1).LineColor(primaryColor);
                            
                            mainCol.Item().Row(row =>
                            {
                                row.AutoItem().Text("Список на лица осигурани по полиса за патничко осигурување ");
                                row.AutoItem().Text(" PZO 080224").Bold();
                            });
                            mainCol.Item().Row(row =>
                            {
                                row.AutoItem().Text("List of insured persons by the travel insurance policy");
                                row.AutoItem().Text(" PZO 080224").Bold();
                            });
                            
                            mainCol.Item().PaddingVertical(5);

                            var tableHeaders = new List<TableHeader>()
                            {
                                new TableHeader() { NameMK = "Р.Бр", NameEn = "No." },
                                new TableHeader() { NameMK = "Презиме и име", NameEn = "Name of the insurer" },
                                new TableHeader() { NameMK = "ЕМБГ", NameEn = "EMBG" },
                                new TableHeader() { NameMK = "Пасош број", NameEn = "Passport No." },
                                new TableHeader() { NameMK = "Спортски ризик", NameEn = "Sports Risks" },
                                new TableHeader() { NameMK = "Covid 19", NameEn = "Covid 19" },
                                new TableHeader() { NameMK = "Професионален возач", NameEn = "Professional driver" },
                                new TableHeader() { NameMK = "Студент во странство", NameEn = "Students abroad" },
                            };
                            
                            var tableData = new List<Insurer>()
                            {
                                new Insurer()
                                {
                                    OrderNumber = "1", EMBG = "1111111111111", Name = "Nikola Stankovski",
                                    PassportNo = "11111111", SportRisk = "/", Covid19 = "/", ProfessionalDriver = "/",
                                    StudentAbroad = "/"
                                },
                                new Insurer()
                                {
                                    OrderNumber = "2", EMBG = "2222222222222", Name = "Elena Mojsova",
                                    PassportNo = "22222222", SportRisk = "/", Covid19 = "/", ProfessionalDriver = "/",
                                    StudentAbroad = "/"
                                },
                                new Insurer()
                                {
                                    OrderNumber = "3", EMBG = "3333333333333", Name = "Filip Petrushevski",
                                    PassportNo = "33333333", SportRisk = "/", Covid19 = "/", ProfessionalDriver = "/",
                                    StudentAbroad = "/"
                                },
                            };
                            
                            mainCol.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(30);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                    columns.ConstantColumn(40);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });
                            
                                table.Header(tableHeader =>
                                {
                                    foreach (var th in tableHeaders)
                                    {
                                        tableHeader.Cell().Element(Block).Column(col =>
                                        {
                                            col.Item().Text(th.NameMK);
                                            col.Item().Text(th.NameEn);
                                        });
                                    }
                                });

                                foreach (var td in tableData)
                                {
                                    foreach (var prop in td.GetType().GetProperties())
                                    {
                                        table.Cell().Element(Entry).Text(prop.GetValue(td));
                                    }
                                }
                            });
                            
                            mainCol.Item().PaddingVertical(10).LineHorizontal(1).LineColor(primaryColor);

                            mainCol.Item().Text("Незгода").Bold();
                            mainCol.Item().Row(row =>
                            {
                                row.AutoItem().Text("Осигуреник/Insured:");
                                row.AutoItem().Text(" Сите лица").Bold();
                            });

                            mainCol.Item().PaddingVertical(10);
                            
                            mainCol.Item().Row(row =>
                            {
                               row.RelativeItem().AlignMiddle().Column(col =>
                               {
                                   col.Item().PaddingBottom(5).Text("Датум и место на изготвување / Date and place of issue").AlignCenter();
                                   col.Item().Text("Скопје, 22.07.2022").Bold().AlignCenter();
                               }); 
                               row.RelativeItem().AlignMiddle().Column(col =>
                               {
                                   col.Item().PaddingBottom(5).Text("Вкупна премија / Total premium").AlignCenter();
                                   col.Item().Text("1274 ден").Bold().AlignCenter();
                               }); 
                            });
                            
                            mainCol.Item().PaddingVertical(10);

                            mainCol.Item()
                                .Text(
                                    "Осигурувањето е склучено согласно Условите за патничко осигурување У-ПАТН 01/2021 бр. 02-20225/19 од 01.07.2021 година со датум на примена од 01.08.2021 кои му се предадени на договорувачот на осигурувањето по електронски пат.")
                                .Italic();
                            mainCol.Item()
                                .Text(
                                    "Општите услови за осигурување на лица од последици на несреќен случај (незгода) НОУ-01/2021 бр. 02-20225/3 од 01.07.2021 година со датум на примена од 01.08.2021 кои му се предадени на договорувачот на осигурувањето по електронски пат.")
                                .Italic();

                            mainCol.Item().PaddingVertical(10);

                            mainCol.Item()
                                .Text("Телефонски број за контакт со Асистентската компанија: +389 2 5101-525")
                                .Italic();
                            mainCol.Item()
                                .Text("Сите права по оваа полиса му припаѓаат на осигуреникот")
                                .Italic();
                            mainCol.Item()
                                .Text("Согласно законот за ДДВ, член 23, точка 6, дејноста осигурување е ослободена од плаќање на данок без право на одбиток на претходен данок")
                                .Italic();
                            mainCol.Item()
                                .Text("Осигурувачот го задржува правото на пресметковна и друга грешка")
                                .Italic();
                            
                            mainCol.Item().PaddingVertical(10);

                            mainCol.Item()
                                .Text("Го овластувам Осигурувачот во случај на поднесено побарување да бара податоци за мојата здравствена состојба и лекување. Во случај на поднесено оштетно побарување ги ослободува здравствените институции и мојот матичен лекар да ги дадат бараните податоци на Осигурувачот.")
                                .Italic();
                        });
                    
                    page.Footer().Column(footerCol =>
                    {
                        footerCol.Item().Row(row =>
                        {
                            row.Spacing(20);
                            row.RelativeItem().AlignMiddle().AlignCenter().Column(col =>
                            {
                                col.Item().Text("• Ова осигурување е договорено преку ИНТЕРНЕТ;").FontSize(6);
                                col.Item().Text("• Договорувачот на осигурувањето мора своерачно да ја потпише оваа полиса;").FontSize(6);
                                col.Item().Text("• За сите барања поврзани со покритието користете го бројот на ПОЛИСА;").FontSize(6);
                                col.Item().Text("• Оваа полиса е издадена по електронски пат и има важност без печат и потпис од осигурувачот;").FontSize(6);
                            });
                                
                            row.RelativeItem().AlignCenter().Column(col =>
                            {
                                col.Spacing(30);
                                    
                                col.Item().AlignMiddle().AlignCenter().Text("Договорувач");
                                col.Item().Width(140).LineHorizontal(1);
                            });
                        });
                    });
                });
            })
            .GeneratePdf(filePath);
        }
}

