import HttpClient from "../../utils/httpClient.ts";
import {Book} from "./models.ts";

const httpClient = new HttpClient("/");

export async function getBooks() {
    try {
        return await httpClient.get<Book[]>("api/_react/Books", true);
    } catch (error) {
        console.error("Failed to load resource:", error);
        return [];
    }
}

export async function addBook(payload: Book) {
    try {
        return await httpClient.post<Book>("api/_react/Books/add", payload, true);
    } catch(error) {
        console.error("Failed to load resource:", error);
        return null;
    }
}