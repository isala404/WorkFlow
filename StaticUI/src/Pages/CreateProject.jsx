import React from "react";
import { getCurrentCompany } from "../libs/getCompany"

const CreateProject = () => {
    return (
        <div className="flex flex-grow flex-col bg-gray-100 p-5 md:w-4/5 xl:w-2/5 m-auto mt-20">
            <h1 className="text-2xl text-center">Create a new Project</h1>
            <form>
                <div class="mb-6">
                    <label for="basic-url" class="text-grey-darker inline-block mb-2">Name</label>
                    <input type="text" id="name" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5" placeholder="My Project" required={true} />
                </div>
                <label for="basic-url" class="text-grey-darker inline-block mb-2">Project URI</label>
                <div class="flex flex-wrap items-stretch w-full mb-4 relative">
                    <div class="flex -mr-px">
                        <span class="flex items-center leading-normal bg-grey-lighter rounded rounded-r-none border border-r-0 border-grey-light px-3 whitespace-no-wrap text-grey-dark text-sm">https://workflow.isala.me/{getCurrentCompany()}/</span>
                    </div>
                    <input type="text" class="flex-shrink flex-grow flex-auto leading-normal w-px flex-1 border h-10 border-grey-light rounded rounded-l-none px-3 relative" pattern="[a-z0-9-]+" placeholder="my-project" />
                </div>
                <button type="submit" class="block text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 w-full">Submit</button>
            </form>
        </div>
    )
}

export default CreateProject;