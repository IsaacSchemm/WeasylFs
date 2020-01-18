namespace WeasylFs.Endpoints

open WeasylFs
open System
open System.Net

module UserGallery =
    type Request() =
        member val Since = Nullable<DateTimeOffset>() with get, set
        member val Count = Nullable<int>() with get, set
        member val FolderId = Nullable<int>() with get, set
        member val BackId = Nullable<int>() with get, set
        member val NextId = Nullable<int>() with get, set

        member this.QueryString =
            seq {
                if this.Since.HasValue then
                    yield ("since", this.Since.Value.ToString("o"))
                if this.Count.HasValue then
                    yield ("count", this.Count.Value.ToString())
                if this.FolderId.HasValue then
                    yield ("folderid", this.FolderId.Value.ToString())
                if this.BackId.HasValue then
                    yield ("backid", this.BackId.Value.ToString())
                if this.NextId.HasValue then
                    yield ("nextid", this.NextId.Value.ToString())
            }
            |> Util.BuildQueryString

    type Response = {
        submissions: Submission list
        backid: int option
        nextid: int option
    } with
        member this.NullableBackId =
            this.backid
            |> Option.toNullable
        member this.NullableNextId =
            this.nextid
            |> Option.toNullable

    let AsyncExecute credentials (parameters: Request) username =
        sprintf "api/users/%s/gallery?%s" (WebUtility.UrlEncode username) (parameters.QueryString)
        |> Util.CreateRequest credentials
        |> Util.AsyncReadJson<Response>

    let ExecuteAsync credentials parameters username =
        AsyncExecute credentials parameters username
        |> Async.StartAsTask