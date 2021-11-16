/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useState } from 'react';

const DropDownMenu = (props) => {
    const [profileOptions, setProfileOptions] = useState(false);
    return (
        <nav class="bg-white dark:bg-gray-800 shadow ">
            <div class="max-w-7xl mx-auto px-8">
                <div class="flex items-center justify-between h-16">
                    <div class=" flex items-center">
                        <a class="flex-shrink-0" href="/">
                            <img class="h-8 w-8" src="icons/rocket.svg" alt="Workflow" />
                        </a>
                        <div class="hidden md:block">
                            <div class="ml-10 flex items-baseline space-x-4">
                                <NavOptions />
                            </div>
                        </div>
                    </div>
                    <div class="block">
                        <div class="ml-4 flex items-center md:ml-6">
                            <div class="hidden md:block">
                                <CompanySelect />
                            </div>

                            <div class="ml-3 relative">
                                <div class="relative inline-block text-left">
                                    <div>
                                        <button type="button" class="flex items-center justify-center w-full rounded-md  px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-50 hover:bg-gray-50 dark:hover:bg-gray-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-gray-500" id="options-menu" onClick={() => setProfileOptions(!profileOptions)}>
                                            <img src="https://avatars.dicebear.com/api/bottts/isala.svg" alt="profile-pic" height="40" width="40" />
                                        </button>
                                    </div>
                                    {profileOptions && (
                                        <div class="origin-top-right absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white dark:bg-gray-800 ring-1 ring-black ring-opacity-5">
                                            <div class="py-1 " role="menu" aria-orientation="vertical" aria-labelledby="options-menu">
                                                <a href="#" class="block block px-4 py-2 text-md text-gray-700 hover:bg-gray-100 hover:text-gray-900 dark:text-gray-100 dark:hover:text-white dark:hover:bg-gray-600" role="menuitem">
                                                    <span class="flex flex-col">
                                                        <span>
                                                            Settings
                                                        </span>
                                                    </span>
                                                </a>
                                                <a href="#" class="block block px-4 py-2 text-md text-gray-700 hover:bg-gray-100 hover:text-gray-900 dark:text-gray-100 dark:hover:text-white dark:hover:bg-gray-600" role="menuitem">
                                                    <span class="flex flex-col">
                                                        <span>
                                                            Account
                                                        </span>
                                                    </span>
                                                </a>
                                                <a href="#" class="block block px-4 py-2 text-md text-gray-700 hover:bg-gray-100 hover:text-gray-900 dark:text-gray-100 dark:hover:text-white dark:hover:bg-gray-600" role="menuitem">
                                                    <span class="flex flex-col">
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
                    <div class="-mr-2 flex md:hidden">
                        <button class="text-gray-800 dark:text-white hover:text-gray-300 inline-flex items-center justify-center p-2 rounded-md focus:outline-none">
                            <svg width="20" height="20" fill="currentColor" class="h-8 w-8" viewBox="0 0 1792 1792" xmlns="http://www.w3.org/2000/svg">
                                <path d="M1664 1344v128q0 26-19 45t-45 19h-1408q-26 0-45-19t-19-45v-128q0-26 19-45t45-19h1408q26 0 45 19t19 45zm0-512v128q0 26-19 45t-45 19h-1408q-26 0-45-19t-19-45v-128q0-26 19-45t45-19h1408q26 0 45 19t19 45zm0-512v128q0 26-19 45t-45 19h-1408q-26 0-45-19t-19-45v-128q0-26 19-45t45-19h1408q26 0 45 19t19 45z">
                                </path>
                            </svg>
                        </button>
                    </div>
                </div>
            </div>

            <div class="md:hidden">
                <div class="px-2 pt-2 pb-3 space-y-1 sm:px-3">
                    <NavOptions />
                    <CompanySelect />
                </div>
            </div>
        </nav>
    );
};

const NavOptions = () => {
    return (
        <>
            <a class="text-gray-800 dark:text-white block px-3 py-2 rounded-md text-base font-medium" href="/#">
                Home
            </a>
            <a class="text-gray-300 hover:text-gray-800 dark:hover:text-white block px-3 py-2 rounded-md text-base font-medium" href="/#">
                Gallery
            </a>
            <a class="text-gray-300 hover:text-gray-800 dark:hover:text-white block px-3 py-2 rounded-md text-base font-medium" href="/#">
                Content
            </a>
            <a class="text-gray-300 hover:text-gray-800 dark:hover:text-white block px-3 py-2 rounded-md text-base font-medium" href="/#">
                Contact
            </a>
        </>
    )
}

const CompanySelect = () => {
    return (
        <select class="block w-52 text-gray-700 py-2 px-3 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-primary-500 focus:border-primary-500 mx-auto" name="animals">
            <option value="">
                Select a company
            </option>
            <option value="dog">
                Dog
            </option>
            <option value="cat">
                Cat
            </option>
            <option value="hamster">
                Hamster
            </option>
            <option value="parrot">
                Parrot
            </option>
            <option value="spider">
                Spider
            </option>
            <option value="goldfish">
                Goldfish
            </option>
        </select>
    )
}


export default DropDownMenu;