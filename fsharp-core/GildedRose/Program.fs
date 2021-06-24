namespace GildedRose

open System.Collections.Generic

type Item = { Name: string; SellIn: int; Quality: int }

type GildedRose(items:IList<Item>) =
    let Items = items

    member this.UpdateQuality() =
        
        let backstagePass = "Backstage passes to a TAFKAL80ETC concert"
        let agedBrie = "Aged Brie"
        let sulfuras = "Sulfuras, Hand of Ragnaros"
        let conjured = "Conjured"
        
        let updateSellIn item =
            if item.Name = sulfuras then
                item
            else
                {item with SellIn = item.SellIn - 1}
                
        let updateBackstagePass item =
            let newQuality =
                match item.SellIn with
                | s when s < 0 -> 0
                | s when s < 5 -> item.Quality + 3
                | s when s < 10 -> item.Quality + 2
                | _ -> item.Quality + 1
            {item with Quality = System.Math.Clamp(newQuality, 0, 50)}
            
        let updateAgedBrie item =
            let newQuality =
                match item.SellIn with
                | s when s < 0 -> item.Quality + 2
                | _ -> item.Quality + 1
            {item with Quality = System.Math.Clamp(newQuality, 0, 50)}
            
        let updateStandardItem item =
            let newQuality =
                match item.SellIn with
                | s when s < 0 -> item.Quality - 2
                | _ -> item.Quality - 1
            {item with Quality = System.Math.Clamp(newQuality, 0, 50)}
            
        let updateConjuredItem item =
            let newQuality =
                match item.SellIn with
                | s when s < 0 -> item.Quality - 4
                | _ -> item.Quality - 2
            {item with Quality = System.Math.Clamp(newQuality, 0, 50)}
            
        let updateQuality item =
            if item.Name = sulfuras then
                item
            else if item.Name = backstagePass then
                updateBackstagePass item
            else if item.Name = agedBrie then
                updateAgedBrie item
            else if item.Name.StartsWith conjured then
                updateConjuredItem item
            else
                updateStandardItem item
                
        let updateItem =
            updateSellIn >> updateQuality
        
        for i = 0 to Items.Count - 1 do
            Items.[i] <- updateItem Items.[i]
        ()


module Program =
    [<EntryPoint>]
    let main argv =
        printfn "OMGHAI!"
        let Items = new List<Item>()
        Items.Add({Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20})
        Items.Add({Name = "Aged Brie"; SellIn = 2; Quality = 0})
        Items.Add({Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7})
        Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80})
        Items.Add({Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49})
        Items.Add({Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49})
        Items.Add({Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6})

        let app = new GildedRose(Items)
        for i = 0 to 30 do
            printfn "-------- day %d --------" i
            printfn "name, sellIn, quality"
            for j = 0 to Items.Count - 1 do
                 printfn "%s, %d, %d" Items.[j].Name Items.[j].SellIn Items.[j].Quality
            printfn ""
            app.UpdateQuality()
        0 