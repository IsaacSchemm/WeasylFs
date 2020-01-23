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
            |> Util.BuildQueryString

    type Response = {
        rating: string
        link: string
        owner: string
        owner_login: string
        submitid: int
        title: string
        media: Map<string, Media list>
        posted_at: DateTimeOffset
        subtype: string
        ``type``: string
        
        embedlink: string option
        description: string
        tags: string list
        comments: int
        owner_media: Map<string, Media list>
        favorited: bool
        folder_name: string option
        favorites: int
        folderid: int option
        views: int
        friends_only: bool
    } with
        member this.EmbedLinkOrNull =
            this.embedlink
            |> Option.toObj
        member this.FolderNameOrNull =
            this.folder_name
            |> Option.toObj
        member this.FolderIdOrNull =
            this.folderid
            |> Option.toNullable

    let AsyncExecute credentials (parameters: Request) submitid =
        sprintf "api/submissions/%d/view?%s" submitid parameters.QueryString
        |> Util.CreateRequest credentials
        |> Util.AsyncReadJson<Response>

    let ExecuteAsync credentials parameters submitid =
        AsyncExecute credentials parameters submitid
        |> Async.StartAsTask