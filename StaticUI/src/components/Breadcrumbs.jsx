/* eslint-disable jsx-a11y/anchor-is-valid */
import React from 'react';
import { useLocation } from 'react-router-dom'

const Breadcrumbs = () => {
    const location = useLocation();
    const URLs = location.pathname.substring(1).split('/');
    let items = URLs.map(item => {
        if (item === '') {
            return null;
        }
        return {
            name: toTitleCase(item),
            url: `${URLs.slice(1, URLs.indexOf(item) + 1).join('/')}`
        }
    }).filter(item => item !== null);
    items.unshift({ name: "Home", url: "/" })
    console.log("items", items)
    return (
        <div className="my-5">
            <ol className="list-reset flex text-grey-dark">
                {items.map((item) => {
                    return (
                        <>
                            <li><a href={item.url} className="font-bold">{item.name}</a></li>
                            <li><span className="mx-2">{">"}</span></li>
                        </>
                    )
                })}

            </ol>
        </div>
    );
}

function toTitleCase(str) {
    console.log("fgfg", str)
    return str.replace(
        /\w\S*/g,
        function (txt) {
            return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
        }
    );
}

export default Breadcrumbs;