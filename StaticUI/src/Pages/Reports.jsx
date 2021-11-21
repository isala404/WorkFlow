import React, { useEffect, useRef } from "react";
import Chart from 'chart.js/auto';

const Home = () => {
    const nProjects = useRef(null);
    const nPeople = useRef(null);


    useEffect(() => {
        const ctx = nProjects.current.getContext("2d");
        const chart = new Chart(ctx, {
            type: "line",
            data: {
                labels: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
                datasets: [{
                    data: [86, 114, 106, 106, 107, 111, 133],
                    label: "Number of Projects",
                    borderColor: "#3e95cd",
                    backgroundColor: "#7bb6dd",
                    fill: true,
                }]
            },
        });
        return () => {
            chart.destroy();
        }
    })

    useEffect(() => {
        const ctx = nPeople.current.getContext("2d");
        const chart = new Chart(ctx, {
            type: "line",
            data: {
                labels: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
                datasets: [{
                    data: [70, 90, 44, 60, 83, 90, 100],
                    label: "Number of People",
                    borderColor: "#3cba9f",
                    backgroundColor: "#71d1bd",
                    fill: true,
                }]
            },
        });
        return () => {
            chart.destroy();
        }
    })

    return (
        <div className="flex flex-col m-auto">
            <div className="flex flex-row justify-between mb-2 w-96 ml-auto mr-10 xl:mr-48">
                <div className="w-full mr-3">
                    <label for="startDate" class="text-grey-darker inline-block mb-2">Start Date</label>
                    <input type="date" id="startDate" name="startDate" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full m-0 p-2.5" placeholder="31/10/2021" required={true} />
                </div>
                <div className="w-full ml-3">
                    <label for="endDate" class="text-grey-darker inline-block mb-2">End Date</label>
                    <input type="date" id="endDate" name="endDate" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full m-0 p-2.5" placeholder="31/10/2021" required={true} />
                </div>
            </div>
            <div className="w-100 xl:w-3/6 m-auto">
                <canvas
                    ref={nProjects}
                />
            </div>
            <div className="w-100 xl:w-3/6 m-auto">
                <canvas
                    ref={nPeople}
                />
            </div>
        </div>
    )
}

export default Home;