import React from "react";

const UserSettings = () => {
    return (
        <div className="flex flex-col items-center" style={{ height: "80vh" }}>
            <div className="inline-block min-w-100">
                <div className="bg-gray-100 w-96 h-auto my-10 w-auto p-5">
                    <h2 className="text-xl ml-8">Update Profile</h2>
                    <form>
                        <div className="flex justify-around mt-8">
                            <div className="mx-3 lg:mx-10">
                                <div class="mb-6">
                                    <label for="name" class="text-sm font-medium text-gray-900 block mb-2">Name</label>
                                    <input type="text" id="name" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full lg:w-80 p-2.5" required={true} />
                                </div>
                                <div class="mb-6">
                                    <label for="email" class="text-sm font-medium text-gray-900 block mb-2">Email</label>
                                    <input type="email" id="email" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full lg:w-80 p-2.5" required={true} />
                                </div>
                            </div>
                            <div className="mx-3 lg:mx-10">
                                <div class="mb-6">
                                    <label for="password" class="text-sm font-medium text-gray-900 block mb-2">Password</label>
                                    <input type="password" id="password" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full lg:w-80 p-2.5" required={true} />
                                </div>
                                <div class="mb-6">
                                    <label for="confirmPassword" class="text-sm font-medium text-gray-900 block mb-2">Confirm Password</label>
                                    <input type="password" id="confirmPassword" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full lg:w-80 p-2.5" required={true} />
                                </div>
                            </div>
                        </div>
                        <div className="flex flex-col items-center">
                            <button type="submit" class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center w-80">Submit</button>
                        </div>
                    </form>
                </div>
                <div className="bg-gray-100 my-10 w-auto p-5">
                    <h2 className="text-xl ml-8">Companies</h2>
                    <Company name={"Netflix"} />
                    <Company name={"Cloudflare"} />
                    <Company name={"Iconicto"} />
                </div>
            </div>
        </div>
    )
}

const Company = ({ name }) => {
    return (
        <div className="w-100 m-4 p-2 border border-black bg-white">
            {name}
            <button class="bg-red-400 text-white rounded-full h-6 px-3 float-right ml-3">Leave</button>
            <button class="bg-purple-500 text-white rounded-full h-6 px-3 float-right ml-3">Edit</button>
        </div>
    )
};

export default UserSettings;