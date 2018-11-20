# Hoofdstuk 8 MVC - Oefening: Sportstore

## De Oplossing:
### Product Overzicht
> Surfen naar /Product/Index geeft een overzicht van alle producten in alfabetische volgorde. 
![Index.png](/docs/images/product_index.png "Product Overzicht")

### Product Wijzigen
![Edit.png](/docs/images/product_edit.png "Product Aanmaken/wijzigen")
> Opm: in het volgende hoofdstuk zien we hoe de labels kunnen worden aangepast gebruik makend van annotaties. Volgend hoofdstuk behandelt ook validatie. Momenteel mag je er van uitgaan dat de gebruiker alles correct invult. 

### Product Toevoegen
![Create.png](/docs/images/product_create.png "Product Aanmaken")
> Let wel: het availability veld is niet van in het begin van toepassing.
 
### Product Verwijderen
![Delete.png](/docs/images/product_delete.png "Product Verwijderen")

## Achtergrond informatie
De applicatie is een ASP.NET Web Application gebaseerd op een MVC template. De Data en Domain folders werden overgenomen uit de vorige oefening hoofdstuk 7. Ook de unit testen werden uit dit project overgenomen. Enkele belangrijke wijzigingen in het domein tegenover hoofdstuk 7:
- `Product` – `Category` is nu een 1- n verband 
- Geen `OnlineProduct` die erft van Product 
![dcd.png](/docs/images/dcd.png "DCD")

## Stappenplan
### Quickstart
- Clone [deze repository](https://github.com/WebIII/08exSportsStore_Part1.git)
- Run de unit testen, deze moeten allen slagen.
- Run de applicatie. Je komt op de default home pagina terecht en de databank is aangemaakt.

### Repository Pattern
#### ProductRepository
- In de Domain-laag definieer je de Interface `IProductRepository`: bevat de CRUD operaties (ophalen alle producten, ophalen van 1 product o.b.v. id, toevoegen en verwijderen van een product, persisteren van de wijzigingen)
- In de Data-laag maak je in een folder `Repositories` een concrete klasse `ProductRepository` aan die gebruik maakt van de `ApplicationDbContext` en `IProductRepository` implementeert.
- Configureer de dependency injection in `Startup.cs`, de interface  `IProductRepository` wordt per request automatisch geresolved door een `ProductRepository`.

#### CategoryRepository
- In de Domain-laag definieer je de Interface `ICategoryRepository`
bevat methodes voor het opvragen van alle categoriën en 1 categorie o.b.v. de `categorieid`. 
- In de Data-laag maak je in een folder `Repositories` een concrete klasse `CategoryRepository` aan die gebruik maakt van de `ApplicationDbContext` en de `ICategoryRepository` interface implementeert. 
- Configureer de dependency injection in `Startup.cs`, de interface  `ICategoryRepository` wordt per request automatisch geresolved door een `CategoryRepository`.

 
### MVC Pattern
#### ProductController - Index Action
- Maak in de `Controllers` folder een Controller klasse `ProductController` aan.  
- Pas de routing aan zodat de startpagina van de site /Product/Index wordt. 
- Voeg in de `navbar` van `_Layout` een link genaamd ‘Products’ toe, zorg dat deze ook direct naar /Products/Index navigeert.
- De `ProductController` zal nood hebben aan een `IProductRepository`. Declareer een field `_productRepository`. 
- Maak de constructor voor de `ProductController` aan. 
- Implementeer de Index action method.
- Voeg in de Views/Product folder een  View `Index.cshtml` toe.
- Pas de view aan zodat ze overeenkomt met het voorbeeld. “New Product” is een link maar opgemaakt met bootstrap classes. 
- Je kan nu je programma runnen en het resultaat bekijken. 

#### ProductController - Edit Actions
- Op de screenshots kan je zien wat er getoond moet worden op de Edit pagina. Maak een `Viewmodel` aan die de controller zal gebruiken om de nodige informatie door te geven aan de view. Maak hiervoor in je project een nieuwe subfolder `ProductViewModels` aan in de folder `Models` en voeg een klasse `EditViewModel` toe met de gepaste properties en constructor(s).  
- Aangezien de gebruiker een categorie dient te selecteren uit een lijst, dien je in de `ProductController` toegang te hebben tot de `CategoryRepository`. Verzorg dit via constructor injectie. 
- Maak een `Edit` (`HttpGet`) action method aan en implementeer. Je kan de lijst van categorieën naar de view doorgeven via `ViewData`. 
- Maak de bijhorende `Edit.cshtml` View aan en pas aan zodat ze overeenkomt met het voorbeeld (zie hoger).
- Je kan nu je programma runnen en het resultaat bekijken. 
- Maak een `Edit` (`HttpPost`) action method aan en implementeer. 
- Run de applicatie en test het editeren uit. 
 
#### ProductController - Create Actions
- Maak een `Create` (`HttpGet`) action method aan en implementeer. Maak ook hier gebruik van `Product` - `EditViewModel` en de `Edit` View, je hoeft hier slechts enkele kleine aanpassingen te doen. 
- Je kan nu je programma runnen en het resultaat bekijken. 
- Maak een `Create` (`HttpPost`) action method aan en implementeer. 
- Run de applicatie en test de creatie uit. 

#### ProductController - Delete Actions
- Maak een `Delete` (`HttpGet`) action method aan en implementeer 
- Maak de bijhorende View aan (zie voorbeeld).
- Run het programma en bekijk het resultaat. 
- Maak een `Delete` (`HttpPost`) action method aan en implementeer. 
- Run de applicatie en test deze uit. 


Additionele vragen van de klant
1. De klant wenst per `product` ook bij te houden of een `product` enkel in de winkels of enkel in de online shop of in beide kan verkocht worden. 
    - Maak een enum `Availability` aan met de volgende waarden: 
        - `ShopOnly`
        - `OnlineOnly`
        - `ShopAndOnline` 

    - Voeg een property `Availability` aan de `Product` klasse. Wijzig waar nodig. Voor een nieuw `product` is de `Availability` per default gelijk aan `ShopAndOnline`. 
    - Indien nodig, pas de `mapping` naar de database aan. Deze property moet worden gemapt naar een kolom `Availability` van het type `int`, **verplicht** in te vullen. 
    - Pas de seeding van de producten aan in de `SportsStoreDataInitializer`. Het product Kayak is enkel online beschikbaar, de Lifejacket is enkel in de shop beschikbaar. 

    - Pas de `Index` methode en `Index.cshtml` aan. Het overzicht bevat een extra kolom. In volgend hoofdstuk zien we hoe je via annotaties de `display` tekst van een enumeratie kan aanpassen. 

    - Pas de Edit/Create methodes aan en de bijhorende View en ViewModel. In de select kan je gebruik maken van de Html helper methode `GetEnumSelectList` om de items in als dropdownlist te krijgen:  

        ```csharp
        <select ... asp-items="Html.GetEnumSelectList<Availability>()" ... > 
        <option value="">-- select availability --</option> 
        </select>  
        ```
        > Meer infomation over `GetEnumSelectList`  kan je [hier](https://www.jerriepelser.com/blog/using-enum-aspnet-5-select-taghelper/) vinden.
  

2. Is het mogelijk om op de index pagina een categorie te kiezen zodat enkel de producten die behoren tot de gekozen categorie getoond worden? 
    - Pas aan waar nodig zodat je het volgende resultaat bekomt:
    ![Filtering.png](/docs/images/product_filtering.png "Product Filtering")

    Enkele tips : 

    - Voeg een extra parameter categoryId, met defaultwaarde 0, toe aan de methode Index.  

    - Via de ViewData kan je de lijst van categoriën en de geselecteerde categorieId doorgeven aan de View (vb ViewData[“Categories”] en ViewData[“CategoryId”]) 

    - In de View dien je een formulier aan te maken.  
