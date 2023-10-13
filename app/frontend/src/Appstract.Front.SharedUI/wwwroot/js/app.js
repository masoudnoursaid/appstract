function StartBlazor() {
    let loadedCount = 0;
    const resourcesToLoad = [];
    let startTime = performance.now();
    Blazor.start({
        loadBootResource:
            function (type, filename, defaultUri, integrity) {
                if (type == "dotnetjs")
                    return defaultUri;

                const fetchResources = fetch(defaultUri, {
                    cache: 'no-cache',
                    integrity: integrity,
                    headers: {'MyCustomHeader': 'My custom value'}
                });

                resourcesToLoad.push(fetchResources);

                fetchResources.then((r) => {
                    loadedCount += 1;
                    if (filename == "blazor.boot.json") {
                        document.getElementById('progress-bar').style.display = "block";
                        return;
                    }
                    const totalCount = resourcesToLoad.length;
                    const percentLoaded = 10 + parseInt((loadedCount * 90.0) / totalCount);
                    const percentage = document.getElementById('percentage')
                    document.getElementById('progress-bar').style.display = "none";
                    const progressbarContainer = document.getElementById('progress-bar-container')
                    const progressbar = document.getElementById('progressbar');
                    progressbarContainer.style.display = "block";
                    percentage.style.display = "block";
                    progressbar.style.width = percentLoaded + '%';
                    percentage.innerText = percentLoaded + '%';

                    // Calculate remaining minutes
                    const elapsedTime = performance.now() - startTime;
                    const averageTimePerResource = elapsedTime / loadedCount;
                    const remainingCount = totalCount - loadedCount;
                    const remainingTimeMs = remainingCount * averageTimePerResource;
                    const remainingSecond = Math.ceil(remainingTimeMs / 1000);

                    if (remainingSecond >= 60) {
                        percentage.innerText += " - " + Math.ceil(remainingSecond / 60) + " minutes remaining";
                    } else {
                        percentage.innerText += " - " + remainingSecond + " Seconds remaining";
                    }

                });

                return fetchResources;
            }
    });
}

function OpenChat(name, email) {
    window.$crisp = [];
    window.CRISP_WEBSITE_ID = "27f3e0bc-9841-4268-a3af-a818d48fc960";
    (function () {
        d = document;
        s = d.createElement("script");
        s.src = "https://client.crisp.chat/l.js";
        s.async = 1;
        d.getElementsByTagName("head")[0].appendChild(s);
    })();
    $crisp.push(["do", "chat:open"]);
    $crisp.push(["set", "user:email", email]);
    $crisp.push(["set", "user:nickname", name]);
}

poorSpeedIcon = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"12\" height=\"13\" viewBox=\"0 0 12 13\" fill=\"none\">\n" +
    "<mask id=\"mask0_3415_1932\" style=\"mask-type:alpha\" maskUnits=\"userSpaceOnUse\" x=\"0\" y=\"0\" width=\"12\" height=\"13\">\n" +
    "<rect y=\"0.5\" width=\"12\" height=\"12\" fill=\"#D9D9D9\"/>\n" +
    "</mask>\n" +
    "<g mask=\"url(#mask0_3415_1932)\">\n" +
    "<path d=\"M5.99996 11L4.50671 9.49907C4.69966 9.30612 4.9229 9.15548 5.17641 9.04714C5.42994 8.93881 5.70446 8.88464 5.99996 8.88464C6.29547 8.88464 6.56999 8.93881 6.82351 9.04714C7.07703 9.15548 7.30026 9.30612 7.49321 9.49907L5.99996 11Z\" fill=\"#DA1E28\"/>\n" +
    "</g>\n" +
    "</svg>";
goodSpeedIcon = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"12\" height=\"13\" viewBox=\"0 0 12 13\" fill=\"none\">\n" +
    "<mask id=\"mask0_3415_1928\" style=\"mask-type:alpha\" maskUnits=\"userSpaceOnUse\" x=\"0\" y=\"0\" width=\"12\" height=\"13\">\n" +
    "<rect y=\"0.5\" width=\"12\" height=\"12\" fill=\"#D9D9D9\"/>\n" +
    "</mask>\n" +
    "<g mask=\"url(#mask0_3415_1928)\">\n" +
    "<path d=\"M2.78541 7.77777C2.67332 7.66568 2.61984 7.53191 2.62497 7.37647C2.63009 7.22103 2.69099 7.09939 2.80766 7.01157C3.24356 6.65516 3.73234 6.37824 4.27399 6.18081C4.81566 5.98336 5.39024 5.88464 5.99773 5.88464C6.61332 5.88464 7.19194 5.98464 7.73361 6.18464C8.27527 6.38464 8.76405 6.66541 9.19994 7.02696C9.31564 7.11442 9.37502 7.23468 9.37807 7.38774C9.38111 7.5408 9.32687 7.6731 9.21533 7.78463C9.10999 7.88997 8.98142 7.94419 8.82961 7.94729C8.67779 7.95039 8.53842 7.90802 8.41151 7.82021C8.07368 7.55995 7.70121 7.35818 7.29408 7.21492C6.88696 7.07164 6.45522 7.00001 5.99888 7.00001C5.54255 7.00001 5.11117 7.07292 4.70476 7.21876C4.29835 7.36459 3.92879 7.56507 3.59609 7.82021C3.46405 7.91316 3.32239 7.95578 3.17111 7.94809C3.01983 7.9404 2.89127 7.88363 2.78541 7.77777ZM6.00079 10.5481C5.75857 10.5481 5.55525 10.4662 5.39083 10.3023C5.22641 10.1384 5.14419 9.93539 5.14419 9.69317C5.14419 9.45094 5.22612 9.24762 5.38998 9.08321C5.55384 8.91878 5.75689 8.83657 5.99912 8.83657C6.24134 8.83657 6.44467 8.9185 6.60908 9.08236C6.77351 9.24621 6.85572 9.44926 6.85572 9.69148C6.85572 9.93371 6.77379 10.137 6.60993 10.3015C6.44607 10.4659 6.24303 10.5481 6.00079 10.5481Z\" fill=\"#FF832B\"/>\n" +
    "</g>\n" +
    "</svg>";
excellentSpeedIcon = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"12\" height=\"13\" viewBox=\"0 0 12 13\" fill=\"none\">\n" +
    "<mask id=\"mask0_3415_1924\" style=\"mask-type:alpha\" maskUnits=\"userSpaceOnUse\" x=\"0\" y=\"0\" width=\"12\" height=\"13\">\n" +
    "<rect y=\"0.5\" width=\"12\" height=\"12\" fill=\"#D9D9D9\"/>\n" +
    "</mask>\n" +
    "<g mask=\"url(#mask0_3415_1924)\">\n" +
    "<path d=\"M2.78461 7.77694C2.67308 7.6654 2.61987 7.53191 2.625 7.37647C2.63013 7.22103 2.69102 7.09939 2.80769 7.01157C3.24359 6.65516 3.73237 6.37824 4.27402 6.18081C4.81569 5.98336 5.39101 5.88464 5.99999 5.88464C6.61409 5.88464 7.19197 5.98464 7.73364 6.18464C8.2753 6.38464 8.76408 6.66541 9.19998 7.02696C9.31664 7.11477 9.37626 7.23512 9.37883 7.38801C9.38138 7.54089 9.3269 7.6731 9.21536 7.78463C9.10895 7.89104 8.98011 7.94553 8.82884 7.94809C8.67755 7.95065 8.53846 7.90802 8.41154 7.82021C8.07371 7.55995 7.7016 7.35818 7.29519 7.21492C6.88877 7.07164 6.45704 7.00001 5.99999 7.00001C5.54294 7.00001 5.1112 7.07292 4.70479 7.21876C4.29838 7.36459 3.92883 7.56507 3.59613 7.82021C3.46408 7.91316 3.32242 7.95578 3.17114 7.94809C3.01986 7.9404 2.89102 7.88335 2.78461 7.77694ZM0.659613 5.65963C0.553204 5.55322 0.5 5.42358 0.5 5.27069C0.5 5.11782 0.555767 4.99234 0.6673 4.89427C1.38333 4.26927 2.19262 3.77808 3.09519 3.42071C3.99775 3.06333 4.96601 2.88464 5.99999 2.88464C7.03396 2.88464 8.00223 3.06333 8.90479 3.42071C9.80735 3.77808 10.6166 4.26927 11.3327 4.89427C11.4442 4.99234 11.5013 5.11654 11.5038 5.26686C11.5064 5.41717 11.4519 5.5481 11.3404 5.65963C11.234 5.76604 11.1051 5.82101 10.9538 5.82453C10.8026 5.82806 10.666 5.78078 10.5442 5.68271C9.92628 5.15257 9.23156 4.73959 8.46009 4.44376C7.68861 4.14792 6.86858 4.00001 5.99999 4.00001C5.1314 4.00001 4.31136 4.14792 3.53989 4.44376C2.76841 4.73959 2.0737 5.15257 1.45575 5.68271C1.33396 5.78078 1.19742 5.82806 1.04614 5.82453C0.894863 5.82101 0.766021 5.76604 0.659613 5.65963ZM5.99999 10.5481C5.75832 10.5481 5.55528 10.4659 5.39086 10.3015C5.22644 10.137 5.14423 9.93399 5.14423 9.69233C5.14423 9.45066 5.22644 9.24762 5.39086 9.08321C5.55528 8.91878 5.75832 8.83657 5.99999 8.83657C6.24165 8.83657 6.4447 8.91878 6.60911 9.08321C6.77354 9.24762 6.85575 9.45066 6.85575 9.69233C6.85575 9.93399 6.77354 10.137 6.60911 10.3015C6.4447 10.4659 6.24165 10.5481 5.99999 10.5481Z\" fill=\"#24A148\"/>\n" +
    "</g>\n" +
    "</svg>";

function getNetworkQualityCategory(networkQuality) {
    const poorThreshold = 2; // Mbps
    const goodThreshold = 10; // Mbps

    const numericValue = parseFloat(networkQuality);
    let networkQualityDiv = document.getElementById("network__quality");
    let networkStatus = document.getElementById("network__status--text");
    let networkStatusIcon = document.getElementById("network__status--icon");
    document.getElementById("network__speed").innerText = networkQuality;

    if (isNaN(numericValue)) {
        networkStatusIcon.innerHTML = "";
        networkStatus.innerText = "Unknown Speed";
        networkQualityDiv.classList.add("network__speed--unknown");

    } else if (numericValue < poorThreshold) {
        networkStatusIcon.innerHTML = poorSpeedIcon;
        networkStatus.innerText = "Poor Speed";
        networkQualityDiv.classList.add("network__speed--poor");

    } else if (numericValue < goodThreshold) {
        networkStatusIcon.innerHTML = goodSpeedIcon;
        networkStatus.innerText = "Good Speed";
        networkQualityDiv.classList.add("network__speed--good");
    } else {
        networkStatusIcon.innerHTML = excellentSpeedIcon;
        networkStatus.innerText = "Excellent Speed";
        networkQualityDiv.classList.add("network__speed--excellent");
    }
}

function getNetworkQuality() {
    if (navigator.connection) {
        const connection = navigator.connection;

        if (connection.downlink) {
            return connection.downlink + " mbps";
        }
    }

    return "0.0 mbps";
}

window.scrollToBottom = (id) => {
    let element = document.getElementById(id);
    if (element)
        element.scrollTop = element.scrollHeight;
    if (element && element.scrollTop < 1) {
        element.classList.add("sms-conversation__end");
    } else {
        element.classList.remove("sms-conversation__end");
    }
};
