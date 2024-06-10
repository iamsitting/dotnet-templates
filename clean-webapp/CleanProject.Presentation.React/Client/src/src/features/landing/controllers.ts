import {AuthRequestPayload, TokenResponseData} from "./models";
import { setToken } from "../../utils/cookies.ts";
import HttpClient from "../../utils/httpClient.ts";

const httpClient = new HttpClient("/");

export async function sendCredentials(payload: AuthRequestPayload) {
    try {
        const data: TokenResponseData = await httpClient.post<TokenResponseData>("api/Auth/login", payload);
        setToken(data.token);
    } catch (error) {
        console.error("Failed to send credentials:", error);
    }
}

export async function registerUser(payload: AuthRequestPayload) {
    try {
        const data: TokenResponseData = await httpClient.post<TokenResponseData>("api/Auth/register", payload);
        setToken(data.token);
    } catch (error) {
        console.error("Failed to register user:", error);
    }
}