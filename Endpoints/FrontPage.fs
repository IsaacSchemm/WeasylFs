namespace WeasylFs.Endpoints

open WeasylFs
open System

module FrontPage =
    type Request() =
        member val Since = Nullable<DateTimeOffset>() with get, set
        member val Count = Nullable<int>() with get, set

        member this.QueryString =
            seq {
                if this.Since.HasValue then
                    yield ("since", Util.ToIsoString this.Since.Value)
                if this.Count.HasValue then
                    yield ("count", this.Count.Value.ToString())
            }
            |> Util.BuildQueryString

    let AsyncExecute credentials (parameters: Request) =
        sprintf "api/submissions/frontpage?%s" parameters.QueryString
        |> Util.CreateRequest credentials
        |> Util.AsyncReadJson<Submission list>

    let ExecuteAsync credentials parameters =
        AsyncExecute credentials parameters
        |> Async.StartAsTask