module GildedRose.UnitTests

open GildedRose
open System.Collections.Generic
open Xunit
open Swensen.Unquote

type SellIn = int
type Quality = int

let mkItem name (sellIn: SellIn) (quality: Quality) = {
    Name = name
    SellIn = sellIn
    Quality = quality
}

let testItem (expectedSellIn: SellIn) (expectedQuality: Quality) item =
    test <@ expectedSellIn = item.SellIn @>
    test <@ expectedQuality = item.Quality @>

[<Fact>]
let ``Standard items updates`` () =
    let Items = List<Item>()
    mkItem "+5 Dexterity Vest" 1 3
    |> Items.Add
    let app = GildedRose Items
    
    let values : (SellIn * Quality) array = [|
        0, 2
        -1, 0
        -2, 0
    |]
    
    test <@ "+5 Dexterity Vest" = Items.[0].Name @>
    for value in values do
        app.UpdateQuality()
        Items.[0]
        |> (value ||> testItem)

[<Fact>]
let ``Aged Brie item updates`` () =
    let Items = List<Item>()  
    mkItem "Aged Brie" 1 47
    |> Items.Add
    let app = GildedRose Items
    
    let values : (SellIn * Quality) array = [|
        0, 48
        -1, 50
        -2, 50
    |]

    test <@ "Aged Brie" = Items.[0].Name @>
    for value in values do
        app.UpdateQuality()
        Items.[0]
        |> (value ||> testItem)

[<Fact>]
let ``Sulfuras, Hand of Ragnaros item updates`` () =
    let Items = List<Item>()  
    mkItem "Sulfuras, Hand of Ragnaros" 0 80
    |> Items.Add
    let app = GildedRose Items
    
    let values : (SellIn * Quality) array = Array.init 100 (fun _ -> 0, 80)

    test <@ "Sulfuras, Hand of Ragnaros" = Items.[0].Name @>
    for value in values do
        app.UpdateQuality()
        Items.[0]
        |> (value ||> testItem)

[<Fact>]
let ``Backstage passes to a TAFKAL80ETC concert item updates`` () =
    let Items = List<Item>()  
    mkItem "Backstage passes to a TAFKAL80ETC concert" 11 32
    |> Items.Add
    let app = GildedRose Items
    
    let values : (SellIn * Quality) array = [|
        10, 33
        9, 35
        8, 37
        7, 39
        6, 41
        5, 43
        4, 46
        3, 49
        2, 50
        1, 50
        0, 50
        -1, 0
    |]

    test <@ "Backstage passes to a TAFKAL80ETC concert" = Items.[0].Name @>
    for value in values do
        app.UpdateQuality()
        Items.[0]
        |> (value ||> testItem)

[<Fact>]
let ``Conjured items updates`` () =
    let Items = List<Item>()
    mkItem "Conjured Mana Cake" 3 10
    |> Items.Add
    let app = GildedRose Items
    
    let values : (SellIn * Quality) array = [|
        2, 8
        1, 6
        0, 4
        -1, 0
        -2, 0
    |]
    
    test <@ "Conjured Mana Cake" = Items.[0].Name @>
    for value in values do
        app.UpdateQuality()
        Items.[0]
        |> (value ||> testItem)