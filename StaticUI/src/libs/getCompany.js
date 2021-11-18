
export const getCurrentCompany = () => {
    return localStorage.getItem('currentCompany') || null;
}

export const setCurrentCompany = (company) => {
    localStorage.setItem('currentCompany', company);
    // eslint-disable-next-line no-restricted-globals
    location.reload();
}