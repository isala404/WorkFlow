import React from "react";

const Task = ({ name, description, prority, dueDate, estimatedTime, assignee, status = "none" }) => {
    const colorMap = { 'todo': 'bg-gray-400', 'inprogress': 'bg-yellow-100', 'done': 'bg-green-200', 'none': 'bg-gray-50' };
    return (
        <div className={`inline-block whitespace-normal w-96 p-4 border-2 border-black m-2 ${colorMap[status]}`} >
            <span className="float-right"><Prority level={prority} /></span>
            <h2 className="font-bold">{name}</h2>
            <p>{description}</p>
            <div className={"flex mt-3"}>
                <div className="flex">
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                    </svg>
                    {dueDate}
                </div>
                <div className="flex ml-2">

                    <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                    </svg>
                    {estimatedTime}
                </div>
                <div className="flex ml-auto">
                    <img src={`https://avatars.dicebear.com/api/bottts/${assignee}.svg`} alt="profile-pic" height="24" width="24" />
                    {assignee}
                </div>
            </div>
        </div>
    );
}

const Prority = ({ level }) => {
    const colorMap = { 1: '#f50000', 2: '#ffcc00', 3: '#d8d8d8' };
    return (
        <svg xmlns="http://www.w3.org/2000/svg" className="h-6 w-6" fill={`${colorMap[level]}`} viewBox="0 0 24 24" stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 21v-4m0 0V5a2 2 0 012-2h6.5l1 1H21l-3 6 3 6h-8.5l-1-1H5a2 2 0 00-2 2zm9-13.5V9" />
        </svg>
    )
}

export default Task;