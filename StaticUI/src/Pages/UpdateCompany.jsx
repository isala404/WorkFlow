import React from "react";
import { getCurrentCompany } from "../libs/getCompany";

const CreateCompany = () => {
    return (
        <div className="flex flex-col lg:w-2/4 m-auto">
            <div className="flex flex-grow flex-col bg-gray-100 p-5 w-full mt-20">
                <h1 className="text-2xl text-center">Update {getCurrentCompany(true)}</h1>
                <form>
                    <div class="mb-6">
                        <label for="basic-url" class="text-grey-darker inline-block mb-2">Name</label>
                        <input type="text" id="name" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5" placeholder="My Company" required={true} />
                    </div>
                    <label for="basic-url" class="text-grey-darker inline-block mb-2">Company URI</label>
                    <div class="flex flex-wrap items-stretch w-full mb-4 relative">
                        <div class="flex -mr-px">
                            <span class="flex items-center leading-normal bg-grey-lighter rounded rounded-r-none border border-r-0 border-grey-light px-3 whitespace-no-wrap text-grey-dark text-sm">https://workflow.isala.me/</span>
                        </div>
                        <input type="text" class="flex-shrink flex-grow flex-auto leading-normal w-px flex-1 border h-10 border-grey-light rounded rounded-l-none px-3 relative" pattern="[a-z0-9-]+" placeholder="my-company" />
                    </div>
                    <button type="submit" class="block text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 w-full">Update Company</button>
                </form>
            </div>
            <div className="flex flex-grow flex-col bg-gray-100 p-5 w-full m-auto mt-10">
                <h1 className="text-2xl">Manage Users</h1>
                <form className="flex ml-auto -mt-8 mr-2 mb-3">
                    <input type="email" class="border-grey-light rounded rounded-lg px-3 w-48 mr-3" placeholder="user@example.com" required />
                    <button class="bg-blue-300 hover:bg-blue-400 font-semibold py-0 px-4 rounded inline-flex items-center">
                        <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5 m-0" viewBox="0 0 20 20" fill="currentColor">
                            <path d="M2.003 5.884L10 9.882l7.997-3.998A2 2 0 0016 4H4a2 2 0 00-1.997 1.884z" />
                            <path d="M18 8.118l-8 4-8-4V14a2 2 0 002 2h12a2 2 0 002-2V8.118z" />
                        </svg>
                        <span>Invite</span>
                    </button>
                </form>
                <User name="John Doe" role="admin" />
                <User name="Jane Doe" role="user" />
                <User name="John Smith" role="admin" />
                <User name="Jane Smith" role="user" />
            </div>
            <button class="self-end text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 mt-5 w-48">Delete Company</button>
        </div>
    )
}

const User = ({ name, role }) => {
    return (
        <div className="w-100 m-2 p-2 border border-black bg-white">
            {name}
            <button class="bg-red-500 text-white rounded-full h-6 px-3 float-right ml-3">Remove</button>
            <select className="w-18 text-gray-700 px-1 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:ring-primary-500 focus:border-primary-500 mx-auto float-right" defaultValue={role}>
                <option value="admin">
                    Admin
                </option>
                <option value="user">
                    User
                </option>
            </select>
        </div>
    )
};

export default CreateCompany;