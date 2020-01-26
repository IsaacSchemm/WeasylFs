namespace WeasylFs.Endpoints

open WeasylFs
open System

module SubmissionView =
    type Request() =
        member val Anyway = false with get, set
        member val IncrementViews = false with get, set

        member this.QueryString =
            seq {
                if this.Anyway then
                    yield ("anyway", "true")
                if this.IncrementViews then
                    yield ("increment_views", "true")
            }
            |> WeasylUtil.BuildQueryString

    type Response = {
        submitid: int
        subtype: string
        description: string

        rating: string
        link: string
        owner: string
        owner_login: string
        title: string
        media: Map<string, WeasylMedia list>
        posted_at: DateTimeOffset
        ``type``: string
        
        embedlink: string option
        tags: string list
        comments: int
        owner_media: Map<string, WeasylMedia list>
        favorited: bool
        folder_name: string option
        favorites: int
        folderid: int option
        views: int
        friends_only: bool
    } with
        member this.embed_link_or_null =
            this.embedlink
            |> Option.toObj
        member this.folder_name_or_null =
            this.folder_name
            |> Option.toObj
        member this.folder_id_or_null =
            this.folderid
            |> Option.toNullable

    let AsyncExecute credentials (parameters: Request) submitid =
        sprintf "api/submissions/%d/view?%s" submitid parameters.QueryString
        |> WeasylUtil.CreateRequest credentials
        |> WeasylUtil.AsyncReadJson<Response>

    let ExecuteAsync credentials parameters submitid =
        AsyncExecute credentials parameters submitid
        |> Async.StartAsTask