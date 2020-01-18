namespace WeasylFs.Endpoints

open WeasylFs
open System.IO
open FSharp.Json
open System

module ViewSubmission =
    type Request() =
        member val Anyway = false with get, set
        member val IncrementViews = false with get, set

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
    }

    let AsyncExecute credentials (parameters: Request) submitid = async {
        let query = seq {
            if parameters.Anyway then
                yield ("anyway", "true")
            if parameters.IncrementViews then
                yield ("increment_views", "true")
        }
        let qs = Shared.buildQueryString query
        let url = sprintf "/api/submissions/%d/view?%s" submitid qs 
        let req = Shared.createRequest credentials url
        let! resp = req.AsyncGetResponse()
        use sr = new StreamReader(resp.GetResponseStream())
        let! json = sr.ReadToEndAsync() |> Async.AwaitTask
        System.IO.File.WriteAllText("C:/Users/admin/Desktop/out.json", json)
        return Json.deserialize<Response> json
    }

    let ExecuteAsync credentials parameters submitid =
        AsyncExecute credentials parameters submitid
        |> Async.StartAsTask