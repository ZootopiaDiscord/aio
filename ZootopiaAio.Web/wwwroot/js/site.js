// The handful of behaviours that need the window rather than the component's own DOM.

let scrollListener;
let channelLinkListener;

/**
 * Reports to the component whenever the page crosses `threshold`, and returns whether it is
 * currently past it.
 */
export function watchScroll(component, threshold) {
    let scrolled = window.scrollY >= threshold;
    let debounce;

    scrollListener = () => {
        clearTimeout(debounce);
        debounce = setTimeout(() => {
            const now = window.scrollY >= threshold;

            if (now !== scrolled) {
                scrolled = now;
                component.invokeMethodAsync('OnScrolled', now);
            }
        }, 100);
    };

    window.addEventListener('scroll', scrollListener, {passive: true});

    return scrolled;
}

export function stopWatchingScroll() {
    window.removeEventListener('scroll', scrollListener);
    scrollListener = undefined;
}

export function scrollToTop() {
    window.scrollTo({top: 0, behavior: 'smooth'});
}

export function scrollToRules() {
    document.querySelector('.rules')?.scrollIntoView({behavior: 'smooth'});
}

/**
 * Reports to the component when a channel reference anywhere on the page is clicked. The links are
 * plain fragment anchors, so the browser still does the scrolling itself.
 */
export function watchChannelLinks(component) {
    channelLinkListener = event => {
        if (event.target.closest('a.channel')) {
            component.invokeMethodAsync('OnChannelRequested');
        }
    };

    document.addEventListener('click', channelLinkListener);
}

export function stopWatchingChannelLinks() {
    document.removeEventListener('click', channelLinkListener);
    channelLinkListener = undefined;
}
