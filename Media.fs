namespace WeasylFs

type Media = {
    url: string
    mediaid: int option
    links: Map<string, Media list> option
} with
    member this.mediaid_or_null =
        this.mediaid
        |> Option.toNullable
    member this.links_or_empty =
        this.links
        |> Option.defaultValue Map.empty