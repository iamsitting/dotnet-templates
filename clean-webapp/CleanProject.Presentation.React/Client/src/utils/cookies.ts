import Cookies from "universal-cookie";

const cookies = new Cookies();
const AUTH_COOKIE = 'react.token';
export function setToken(token: string) {
    cookies.set(AUTH_COOKIE, token, { sameSite: true });
}

export function getToken(): string | undefined {
    return cookies.get(AUTH_COOKIE);
}