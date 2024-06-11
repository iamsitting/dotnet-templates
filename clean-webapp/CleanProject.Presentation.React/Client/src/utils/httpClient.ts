import { getToken } from './cookies'

class HttpClient {
    private baseURL: string;

    constructor(baseURL: string) {
        this.baseURL = baseURL;
    }

    private getHeaders(authRequired: boolean): HeadersInit {
        const headers: HeadersInit = {
            "Content-Type": "application/json",
        };

        if (authRequired) {
            const token = getToken();
            if (token) {
                headers["Authorization"] = `Bearer ${token}`;
            }
        }

        return headers;
    }

    async post<T>(url: string, body: any, authRequired = false): Promise<T> {
        const res = await fetch(`${this.baseURL}${url}`, {
            method: "POST",
            headers: this.getHeaders(authRequired),
            body: JSON.stringify(body),
        });
        if (!res.ok) {
            throw new Error(`HTTP error! Status: ${res.status}`);
        }
        return await res.json();
    }
    
    async get<T>(url: string, authRequired = false): Promise<T> {
        const res = await fetch(`${this.baseURL}${url}`, {
            method: "GET",
            headers: this.getHeaders(authRequired),
        });
        if(!res.ok) {
            throw new Error(`HTTP error! Status ${res.status}`);
        }
        return await res.json();
    }
}

export default HttpClient;