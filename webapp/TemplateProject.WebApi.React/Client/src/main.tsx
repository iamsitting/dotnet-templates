import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import './index.css'
import Landing from "./features/landing/Landing.tsx";
import BookList from "./features/books/BookList.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <App/>,
        children: [
            {
                path: "",
                element: <Landing/>
            },
            {
                path: "books",
                element: <BookList/>
            }
        ]
    },
    
], {basename: '/React'});

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <RouterProvider router={router} />
  </React.StrictMode>,
)
