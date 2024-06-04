import {AuthRequestPayload, TokenResponseData} from "./models";
import Cookies from "universal-cookie";

export async function sendCredentials(payload: AuthRequestPayload){
    const res = await fetch("/api/Auth/login", {
        method: "post",
        body: JSON.stringify({...payload})
    });
    if(res.ok) {
        const data: TokenResponseData = await res.json()
        const cookies = new Cookies();
        cookies.set(data.token, '', );
    }
}

export async function registerUser(payload: AuthRequestPayload) {
    const res = await fetch("/api/Auth/register", {
        method: "post",
        body: JSON.stringify(payload),
        headers: {
            "Content-Type": "application/json",
        }
    });
    if(res.ok) {
        const data: TokenResponseData = await res.json()
        const cookies = new Cookies();
        cookies.set(data.token, '');
    }
}