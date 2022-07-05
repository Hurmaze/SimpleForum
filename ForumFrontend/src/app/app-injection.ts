import { InjectionToken } from "@angular/core";

export const BASE_API_URL = new InjectionToken<string>("forum api url");
export const USERS_API_URL = new InjectionToken<string>("users api url");
export const TOKENS_API_URL = new InjectionToken<string>("tokens api url");
export const POSTS_API_URL = new InjectionToken<string>("posts api url");
export const FORUM_THREADS_API_URL = new InjectionToken<string>("forum threads api url");
export const STATISTICS_API_URL = new InjectionToken<string>("statistics api url");