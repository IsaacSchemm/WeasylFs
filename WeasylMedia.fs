namespace WeasylFs

type WeasylMedia = {
    url: string
    mediaid: int option
    links: Map<string, WeasylMedia list> option
} with
    member this.mediaid_or_null =
        this.mediaid
        |> Option.toNullable
    member this.links_or_empty =
        this.links
        |> Option.defaultValue Map.empty