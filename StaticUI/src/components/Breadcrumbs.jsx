/* eslint-disable jsx-a11y/anchor-is-valid */
import React from 'react';
import { useLocation } from 'react-router-dom'
import { toTitleCase } from '../libs/getCompany';
import { useParams } from "react-router-dom";

const Breadcrumbs = () => {
    const location = useLocation();
    const { company } = useParams();

    let URLs = location.pathname.substring(1).split('/').filter(item => item !== null && item !== '');
    URLs = URLs.map((item, index) => {
        if (index === 0) {
            return {
                name: toTitleCase(item),
                url: '/'
            }
        }
        return {
            name: toTitleCase(item),
            url: `/${URLs.slice(0, index + 1).join('/')}`
        }
    })
    return (
        <div className="my-5">
            <ol className="list-reset flex text-grey-dark">
                {URLs.map((url) => {
                    return (
                        <>
                            <li><a href={url.url} className="font-bold">{url.name}</a></li>
                            <li><span className="mx-2">{">"}</span></li>
                        </>
                    )
                })}

            </ol>
        </div>
    );
}

export default Breadcrumbs;