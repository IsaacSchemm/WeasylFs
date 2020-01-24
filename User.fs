﻿namespace WeasylFs

open System

type Folder = {
    folder_id: int
    title: string
}

type UserInfo = {
    age: int option
    user_links: Map<string, string list>
    location: string
    gender: string
} with
    member this.age_or_null =
        this.age
        |> Option.toNullable

type User = {
    folders: Folder list
    profile_text: string
    full_name: string
    show_favorites_tab: bool
    suspended: bool
    streaming_status: string
    statistics: Map<string, int> option
    media: Map<string, Media list>
    user_info: UserInfo
    show_favorites_bar: bool
    username: string
    relationship: Map<string, bool>
    banned: bool
    link: string
    recent_type: string
    stream_text: string option
    recent_submissions: Submission list
    catchphrase: string
    created_at: DateTimeOffset
    featured_submission: Submission option
    stream_url: string
    login_name: string
} with
    member this.statistics_or_empty =
        this.statistics
        |> Option.defaultValue Map.empty
    member this.stream_text_or_null =
        this.stream_text
        |> Option.toObj
    member this.featured_submissions =
        match this.featured_submission with
        | Some s -> Seq.singleton s
        | None -> Seq.empty