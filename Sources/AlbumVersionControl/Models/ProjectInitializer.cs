using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AlbumVersionControl.Models
{
    public class ProjectInitializer
    {
        public static ObservableCollection<Project> GetProjectsDataSource()
        {
            return new ObservableCollection<Project>
            {
                new Project
                {
                    Title = "ГТД",
                    Caption = "Грузовая таможенная декларация (ГТД) — основной документ, " +
                              "оформляемый при перемещении товаров через таможенную границу " +
                              "государства (экспорт, импорт). ГТД оформляется распорядителем груза и " +
                              "заверяется таможенным инспектором, в дальнейшем служит основанием для " +
                              "пропуска через границу. В декларации содержатся сведения о грузе и его " +
                              "таможенной стоимости, транспортном средстве, осуществляющем доставку, " +
                              "отправителе и получателе.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Versions = new List<Version>
                    {
                        new Version {Description = "Первая версия документа"}
                    }
                },
                new Project
                {
                    Title = "Провозная ведомость",
                    Caption = "Провозная ведомость - это направление на таможню. Т. е. именно водитель " +
                              "несёт обязательство доставить груз на таможню.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Versions = new List<Version>
                    {
                        new Version {Description = "Первая версия документа"}
                    }
                },
                new Project
                {
                    Title = "CMR",
                    Caption = "Международная товарно-транспортная накладная CMR — это документ, наиболее" +
                              " широко используемый при международных грузоперевозках. CMR выписывается " +
                              "для подтверждения заключения договора перевозки, который определяет ответственность " +
                              "отправителя, перевозчика и получателя товара.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Versions = new List<Version>
                    {
                        new Version {Description = "Первая версия документа"}
                    }
                },
                new Project
                {
                    Title = "Декларация валютного контроля (экспорт)",
                    Caption = "Декларация валютного контроля при экспорте является документом, заполняемым " +
                              "экспортером-продавцом, с помощью которого компетентный орган может проконтролировать" +
                              " перевод в страну суммы в иностранной валюте, полученной в результате торговой сделки," +
                              " в соответствии с условиями платежа и действующими правилами валютного контроля.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Versions = new List<Version>
                    {
                        new Version {Description = "Первая версия документа"}
                    }
                },
                new Project
                {
                    Title = "ДТС",
                    Caption =
                        "Декларация таможенной стоимости (ДТС) - является документом-приложением к соответствующей" +
                        " грузовой таможенной декларации (ГТД) и без нее этот документ недействителен.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Versions = new List<Version>
                    {
                        new Version {Description = "Первая версия документа"}
                    }
                },
                new Project
                {
                    Title = "Свидетельство об осмотре",
                    Caption = "Свидетельство об осмотре - документ, выдаваемый компетентным органом, подтверждающий, " +
                              "что описанные в нем товары были подвергнуты осмотру в соответствии с национальными или " +
                              "международными стандартами согласно законодательству страны, где требуется проведение осмотра, " +
                              "или в соответствии с положениями контракта.",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Versions = new List<Version>
                    {
                        new Version {Description = "Первая версия документа"}
                    }
                }
            };
        }
    }
}