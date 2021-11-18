
export const getCurrentCompany = (pretty=false) => {
    const company = localStorage.getItem('currentCompany') || null;
    if (pretty && company) {
        return toTitleCase(company);
    }
    return company
}

export const setCurrentCompany = (company) => {
    if (company === "new"){
        // eslint-disable-next-line no-restricted-globals
        location.replace("/create/company");
        return
    }
    // eslint-disable-next-line no-restricted-globals
    const newURI = String(location.href).replace(getCurrentCompany(), company);
    localStorage.setItem('currentCompany', company);
    // eslint-disable-next-line no-restricted-globals
    location.replace(newURI);
}

export const toTitleCase = (str) => {
    return decodeURI(str).replace(
        /\w\S*/g,
        function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        }
    );
}
