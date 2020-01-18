namespace WeasylFs

type Media = {
    url: string
    mediaid: int option
    links: Map<string, Media list> option
} with
    member this.NullableMediaId =
        this.mediaid
        |> Option.toNullable
    member this.LinksOrEmpty =
        this.links
        |> Option.defaultValue Map.empty