import 'bootstrap/dist/css/bootstrap.min.css';
import './index.css';
import './layout.css';

import htmx from 'htmx.org'

function updateErrorDiv(htmlContent){
    let errorDiv = document.querySelector("#error-msg");
    errorDiv.insertAdjacentHTML('beforeend', htmlContent);
    htmx.process(errorDiv);
    errorDiv.scrollIntoView();
}

function getAlert(message){
    return `
<div class="alert alert-danger alert-dismissible fade show" role="alert">
    <span>${message}</span>
    <button type="button" class="btn-close" aria-label="Close" onclick="event.target.parentElement.remove()"></button>
</div>`;
}

/* Global error handling */
document.addEventListener("htmx:responseError", (e) => {
    console.error(e.detail.error);
    updateErrorDiv(e.detail.xhr.response)
});

/* Global network error */
document.addEventListener("htmx:sendError", (e) => {
    console.error(e.detail.elt);
    updateErrorDiv(getAlert("Network error: Cannot reach server"));
});