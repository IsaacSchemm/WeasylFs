namespace WeasylFs

open System

type WeasylSubmission = {
    submitid: int option
    subtype: string option

    charid: int option

    rating: string
    link: string
    owner: string
    owner_login: string
    title: string
    media: Map<string, WeasylMedia list>
    posted_at: DateTimeOffset
    ``type``: string
} with
    member this.submitid_or_null =
        this.submitid
        |> Option.toNullable
    member this.subtype_or_null =
        this.subtype
        |> Option.toObj
    member this.charid_or_null =
        this.charid
        |> Option.toNullable