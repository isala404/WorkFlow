/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useState } from 'react';
import { useLocation } from 'react-router-dom'
import { getCurrentCompany, setCurrentCompany } from '../libs/getCompany';

const Navbar = (props) => {
    const [profileOptions, setProfileOptions] = useState(false);
    return (
        <nav className="bg-white dark:bg-gray-800 shadow ">
            <div className="max-w-7xl mx-auto px-8">
                <div className="flex items-center justify-between h-16">
                    <div className=" flex items-center">
                        <a className="flex-shrink-0" href="/">
                            <img className="h-8 w-8" src="/icons/rocket.svg" alt="Workflow" />
                        </a>
                        <div className="hidden md:block">
                            <div className="ml-10 flex items-baseline space-x-4">
                                <NavOptions options={props.options} />
                            </div>
                        </div>
                    </div>
                    <div className="block">
                        <div className="ml-4 flex items-center md:ml-6">
                            <div className="hidden md:block">
                                <CompanySelect />
                            </div>

                            <div className="ml-3 relative">
                                <div className="relative inline-block text-left">
                                    <div>
                                        <button type="button" className="flex items-center justify-center w-full rounded-md  px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-50 hover:bg-gray-50 dark:hover:bg-gray-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-gray-500" id="options-menu" onClick={() => setProfileOptions(!profileOptions)}>
                                            <img src="https://avatars.dicebear.com/api/bottts/isala.svg" alt="profile-pic" height="40" width="40" />
                                        </button>
                                    </div>
                                    {profileOptions && (
                                        <div className="origin-top-right absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white dark:bg-gray-800 ring-1 ring-black ring-opacity-5">
                                            <div className="py-1 " role="menu" aria-orientation="vertical" aria-labelledby="options-menu">
                                                <a href="/user/settings" className="block block px-4 py-2 text-md text-gray-700 hover:bg-gray-100 hover:text-gray-900 dark:text-gray-100 dark:hover:text-white dark:hover:bg-gray-600" role="menuitem">
                                                    <span className="flex flex-col">
                                                        <span>
                                                            Account Settings
                                                        </span>
                                                    </span>
                                                </a>
                                                <a href={`/${getCurrentCompany()}/edit`} className="block block px-4 py-2 text-md text-gray-700 hover:bg-gray-100 hover:text-gray-900 dark:text-gray-100 dark:hover:text-white dark:hover:bg-gray-600" role="menuitem">
                                                    <span className="flex flex-col">
                                                        <span>
                                                            Company Settings
                                                        </span>
                                                    </span>
                                                </a>
                                                <a href="#" className="block block px-4 py-2 text-md text-gray-700 hover:bg-gray-100 hover:text-gray-900 dark:text-gray-100 dark:hover:text-white dark:hover:bg-gray-600" role="menuitem">
                                                    <span className="flex flex-col">
                                                        <span>
                                                            Logout
                                                        </span>
                                                    </span>
                                                </a>
                                            </div>
                                        </div>
                                    )}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="-mr-2 flex md:hidden">
                        <button className="text-gray-800 dark:text-white hover:text-gray-300 inline-flex items-center justify-center p-2 rounded-md focus:outline-none">
                            <svg width="20" height="20" fill="currentColor" className="h-8 w-8" viewBox="0 0 1792 1792" xmlns="http://www.w3.org/2000/svg">
                                <path d="M1664 1344v128q0 26-19 45t-45 19h-1408q-26 0-45-19t-19-45v-128q0-26 19-45t45-19h1408q26 0 45 19t19 45zm0-512v128q0 26-19 45t-45 19h-1408q-26 0-45-19t-19-45v-128q0-26 19-45t45-19h1408q26 0 45 19t19 45zm0-512v128q0 26-19 45t-45 19h-1408q-26 0-45-19t-19-45v-128q0-26 19-45t45-19h1408q26 0 45 19t19 45z">
                                </path>
                            </svg>
                        </button>
                    </div>
                </div>
            </div>

            <div className="md:hidden">
                <div className="px-2 pt-2 pb-3 space-y-1 sm:px-3">
                    <NavOptions options={props.options} />
                    <CompanySelect />
                </div>
            </div>
        </nav>
    );
};

const NavOptions = (options) => {
    const location = useLocation();

    return (
        <>
            {options.options.map((option) => {
                if (option.href !== "/") {
                    option.hrefwithcompany = `/${getCurrentCompany()}${option.href}`
                } else {
                    option.hrefwithcompany = '/'
                }
                if (location.pathname.endsWith(option.href) || location.pathname.split("/")[2] === option.href.split("/")[1]) {
                    return (
                        <a key={option.href} className="text-gray-800 block px-3 py-2 rounded-md text-base font-medium" href={option.hrefwithcompany}>
                            {option.name}
                        </a>
                    )
                }
                else {
                    return (
                        <a key={option.href} className="text-gray-400 hover:text-gray-800 block px-3 py-2 rounded-md text-base font-medium" href={option.hrefwithcompany}>
                            {option.name}
                        </a>
                    )
                }
            })}
        </>
    )
}

const CompanySelect = () => {
    const [company, setCompany] = useState(getCurrentCompany());

    return (
        <select className="block w-52 text-gray-700 py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-primary-500 focus:border-primary-500 mx-auto" name="company" onChange={(e) => { setCompany(e.target.value); setCurrentCompany(e.target.value) }} value={company}>
            <option value="netflix">
                Netflix
            </option>
            <option value="cloudflare">
                Cloudflare
            </option>
            <option value="iconicto">
                Iconicto
            </option>
            <option value="new">
                Create New Company
            </option>
        </select>
    )
}


export default Navbar;