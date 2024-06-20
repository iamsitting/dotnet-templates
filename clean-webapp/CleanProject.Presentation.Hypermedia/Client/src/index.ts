type SwapType = 'innerHTML' | 'outerHTML' | 'beforeend' | 'afterend' | 'beforebegin' | 'afterbegin';
type HXResponse = {
    error: boolean;
    text: string;
    reSwap: SwapType | null;
}
let GET_ELEMENTS: NodeListOf<Element> = document.querySelectorAll('');
let POST_ELEMENTS: NodeListOf<Element> = document.querySelectorAll('');

function _scan() {
    GET_ELEMENTS = document.querySelectorAll("[hx-get]");
    POST_ELEMENTS = document.querySelectorAll("[hx-post]");
}

function _getUrl(el: Element) {
    const location = el.getAttribute('hx-get');
    const url = new URL(location ?? "/");
    const params = [...el.querySelectorAll("input")]
        .filter(x => x.getAttribute('name'))
        .reduce((acc, x) => {
            acc[x.getAttribute('name') as string] = x.value;
            return acc;
        }, {} as Record<string, string>);
    url.search = new URLSearchParams(params).toString();
    return url.toString()
}

function _postUrl(el: Element) {
    const location = el.getAttribute('hx-post');
    return new URL(location ?? "/").toString();
}

function _postBody(el: Element) {
    return [...el.querySelectorAll("input"), ...el.querySelectorAll("select"), ...el.querySelectorAll("textarea")]
        .filter(x => x.getAttribute('name'))
        .reduce((acc, x) => {
            acc[x.getAttribute('name') as string] = x.value;
            return acc;
        }, {} as Record<string, string>);
    
}

function _swap(targetElement: Element, content: string, swap: SwapType | null) {
    document.dispatchEvent(new Event('hx:before-swap'));
    switch (swap) {
        case 'beforebegin':
            targetElement.insertAdjacentHTML('beforebegin', content);
            break;
        case 'afterbegin':
            targetElement.insertAdjacentHTML('afterbegin', content);
            break;
        case 'afterend':
            targetElement.insertAdjacentHTML('afterend', content);
            break;
        case 'beforeend':
            targetElement.insertAdjacentHTML('beforeend', content);
            break;
        case 'outerHTML':
            targetElement.outerHTML = content;
            break;
        case 'innerHTML':
        default:
            targetElement.innerHTML = content;
    }
    document.dispatchEvent(new Event('hx:after-swap'));
    _process();
}

const maxRelocation = 5;

async function _httpGet(location: string, relocations = 0): Promise<HXResponse> {
    document.dispatchEvent(new Event('hx:before-request'));
    const res = await fetch(location, {
        method: 'GET',
        headers: {
            'HX-Request': 'true'
        }
    });
    document.dispatchEvent(new Event('hx:after-request'));
    if (res.ok) {
        const reLocation = res.headers.get("HX-Location");
        if (reLocation) {
            if (relocations < maxRelocation) {
                relocations += 1;
                return _httpGet(reLocation, relocations);
            }
            throw new Error("Max number of relocations hit");
        }
        return {
            error: false,
            text: await res.text(),
            reSwap: res.headers.get('HX-Reswap') as SwapType | null,
        }
    }
    throw new Error(`Returned with status code ${res.status} because of ${res.statusText}`);
}

async function _httpPost(location: string, body: string|null): Promise<HXResponse> {
    document.dispatchEvent(new Event('hx:before-request'));
    const res = await fetch(location, {
        method: 'POST',
        headers: {
            'HX-Request': 'true'
        },
        body
    });
    document.dispatchEvent(new Event('hx:after-request'));
    if(res.ok) {
        const reLocation = res.headers.get("HX-Location");
        let relocations = 0;
        if(reLocation) {
            return _httpGet(reLocation, relocations);
        }
        return {
            error: false,
            text: await res.text(),
            reSwap: res.headers.get("HX-Reswap") as SwapType | null
        }
    }
    throw new Error(`Returned with status code ${res.status} because of ${res.statusText}`);
}

function _process() {
    _scan();

    GET_ELEMENTS.forEach((el) => {
        const hxTrigger = el.getAttribute("hx-trigger") || "click";
        el.addEventListener(hxTrigger, async () => {
            const location = _getUrl(el);

            const result = await _httpGet(location);
            if (result) {
                const hxTarget = el.getAttribute("hx-target");
                const hxSwap = result.reSwap || el.getAttribute("hx-swap") as SwapType | null;
                if (hxTarget !== null) {
                    const target = document.querySelector(hxTarget);
                    if (target) {
                        _swap(target, result.text, hxSwap);
                    }

                }

            }
        })
    });

    POST_ELEMENTS.forEach((el) => {
        let hxTrigger = el.getAttribute("hx-trigger");
        if (!hxTrigger && el.tagName == "form") hxTrigger = "submit";
        else (hxTrigger = 'click')
        
        el.addEventListener(hxTrigger, async () => {
            const location = _postUrl(el);
            const body = JSON.stringify(_postBody(el));
            const result = await _httpPost(location, body);
            if(result) {
                const hxTarget = el.getAttribute("hx-target");
                const hxSwap = result.reSwap || el.getAttribute("hx-swap") as SwapType | null;
                if(hxTarget !== null) {
                    const target = document.querySelector(hxTarget);
                    if(target) {
                        _swap(target, result.text, hxSwap);
                    }
                }
            }
        })

    })
}

function hxLoad() {
    _process();
}

window.hxLoad = hxLoad;