$(function(){ //DOM Ready
	
	//instantiate gridster 
	$(".gridster > ul").gridster({
		widget_margins: [8, 8],
		widget_base_dimensions: [98, 58],
		extra_rows: 20,
		extra_cols: 20,
		min_rows: 30,
		min_cols: 30,
		max_rows: 30,
		resize: {
			enabled: true
		}
	});

	// real requests
	addBurndowns();
	addTasksWithoutPR();
	addSonarMetrics();
	addTaskStatuses();
	addTimeToMergePR();
	addCO2();
	addTeamMood();
	addBugsAndFeatures();
	addCriticalBugsRepair();

	// static data

	addPieProgress("unplannedWorkPercent", 30, '%')
	addPieProgress("unplannedWorkStoryPoints", 17, 'SP')

	addLight('branch1', 'Success', 1, 'Success');
	addLight('branch2', 'Success', 1, 'Success');
	addLight('branch3', 'Success', 1, 'Success');
	addLight('branch4', 'Bad', 1, 'Failure');

	


	addCeremoniesAverageTime();

	//addUnclosedBugsSquadGartner();
	
	// var lis = document.getElementsByTagName('li');
	// var colors = ['red', 'yellow', 'green', 'blue', 'orange', 'pink']

	// for (var i = 0; i < lis.length; i++) {
	// 	lis[i].style.backgroundColor = colors[Math.floor(Math.random()*(lis.length - 1))];
	// }
});



function addUnclosedBugsSquadGartner() {

	var scatterChartData = {

		datasets: [{
			label: 'My First dataset',
			borderColor: 'red',
			backgroundColor: 'red',
			fill: false,
			showLine: false,
			data: [{ x: 5, y: 5}, { x: -5, y: -5}, { x: -3, y: 3}, { x: 2, y: 1}, { x: 1, y: -4}, { x: -4, y: 3}]
		}]
	};
	
	var canvas = document.getElementById('unclosedBugs').getContext('2d');
	new Chart(canvas, {
		type: 'scatter',
		data: scatterChartData,
		options: {
			legend: {
				display: false
			}
		}
	});
}

function addBurndowns() {

	let optimalDataset = {
		label: 'Оптимальный',
		backgroundColor: "green",
		borderColor: "green",
		fill: false
	}

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/BurnDown/1",
		success: (data) => processBurndown("burndownSprint", optimalDataset, data),
		error: (error) => console.error(e)
	})


	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/BurnDown/2",
		success: (data) => processBurndown("burndownEpic", optimalDataset, data),
		error: (error) => console.error(e)
	})


	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/BurnDown/3",
		success: (data) => processBurndown("burndownRelease", optimalDataset, data),
		error: (error) => console.error(e)
	})


	// var datasetsEpic = [optimalDataset, {
	// 	label: 'Текущий',
	// 	data: [100, 73, 65],
	// 	backgroundColor: "blue",
	// 	borderColor: "blue",
	// 	fill: false
	// }];

	// var datasetsRelease = [optimalDataset, {
	// 	label: 'Текущий',
	// 	data: [100, 71, 65, 60, 55, 50, 30, 28],
	// 	backgroundColor: "blue",
	// 	borderColor: "blue",
	// 	fill: false
	// }];


	
	// addLine('burndownEpic', labels, datasetsEpic, 'Story points', 'Days', true);
	// addLine('burndownRelease', labels, datasetsRelease, 'Story points', 'Days', true);
}

function processBurndown(id, optimalDataset, loadedData) {
	let labels = ["0"];
	let localOptimalDataset = optimalDataset;

	var total = 100;
	var step = total / (loadedData.days.length - 1);
	localOptimalDataset.data = [total];

	for (var i = 1; i < loadedData.days.length; i++) {
		total -= step;
		localOptimalDataset.data.push(total);
		labels.push(i);
	}
	
	var datasetsSprint = [optimalDataset, {
		label: 'Текущий',
		data: loadedData.days,
		backgroundColor: "blue",
		borderColor: "blue",
		fill: false
	}];

	addLine(id, labels, datasetsSprint, 'Story points', 'Days', true);

}

function addTasksWithoutPR() {
	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/jiratasks/wopullrequest/1/1",
		success: (data) => {
			var status = data > 2 ? 'Bad' : data > 0 ? 'Normal' : 'Success';
			addLight('tasksWithoutPullRequestProject', status, data, 'Количество');
		},
		error: (error) => console.error(e)
	})

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/jiratasks/wopullrequest/2/1",
		success: (data) => {
			var status = data > 2 ? 'Bad' : data > 0 ? 'Normal' : 'Success';
			addLight('tasksWithoutPullRequestTeam', status, data, 'Количество');
		},
		error: (error) => console.error(e)
	})


	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/jiratasks/wopullrequest/3/1",
		success: (data) => {
			var status = data > 2 ? 'Bad' : data > 0 ? 'Normal' : 'Success';
			addLight('tasksWithoutPullRequestEmployee', status, data, 'Количество');
		},
		error: (error) => console.error(e)
	})

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/jiratasks/wopullrequest/4/1",
		success: (data) => {
			var status = data > 2 ? 'Bad' : data > 0 ? 'Normal' : 'Success';
			addLight('tasksWithoutPullRequestKE', status, data, 'Количество');
		},
		error: (error) => console.error(e)
	})
}

function addSonarMetrics() {


	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/release/TechnicalDebt",
		success: (data) => {

			var datasetsDebt = [{
				label: 'Техдолг',
				data: [],
				backgroundColor: "blue",
				borderColor: "blue",
				fill: false
			}];

			var labels = [], sum = 0;
			for (var i = 0; i < data.length; i++) {
				labels.push('Релиз ' + data[i].releaseID);
				datasetsDebt[0].data.push(data[i].percent);
				sum += data[i].percent;
			}

			addLine('sonarTechnicalDebt', labels, datasetsDebt, '%', 'Релизы', false);

			// TODO обосновать выбор значений
			var currentTechnicalDebt = document.getElementById('currentTechnicalDebt');
			currentTechnicalDebt.innerText = data[data.length - 1].percent + "%";
			if (data[data.length - 1].percent <= 20) {
				currentTechnicalDebt.classList.add('success');
			} else if (data[data.length - 1].percent > 20 && data[data.length - 1].percent < 50) {
				currentTechnicalDebt.classList.add('normal');
			} else {
				currentTechnicalDebt.classList.add('bad');
			}

			var averageTechnicalDebt = document.getElementById('averageTechnicalDebt');
			
			var average = Math.floor(sum / data.length);
			averageTechnicalDebt.innerText = average + "%";
			if (average <= 20) {
				averageTechnicalDebt.classList.add('success');
			} else if (average > 20 && average < 50) {
				averageTechnicalDebt.classList.add('normal');
			} else {
				averageTechnicalDebt.classList.add('bad');
			}

		},
		error: (error) => console.error(e)
	})

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/release/TestCoverage",
		success: (data) => {

			var datasetsTests = [{
				label: 'Покрытие тестами',
				data: [],
				backgroundColor: "blue",
				borderColor: "blue",
				fill: false
			}];

			var labels = [], sum = 0;
			for (var i = 0; i < data.length; i++) {
				labels.push('Релиз ' + data[i].releaseID);
				datasetsTests[0].data.push(data[i].percent);
				sum += data[i].percent;
			}

			addLine('sonarTestCoverage', labels, datasetsTests, '%', 'Months', false);

			var currentTestCoverage = document.getElementById('currentTestCoverage');
			currentTestCoverage.innerText = data[data.length - 1].percent + "%";
			if (data[data.length - 1].percent >= 80) {
				currentTestCoverage.classList.add('success');
			} else if (data[data.length - 1].percent >= 60 && data[data.length - 1].percent < 80) {
				currentTestCoverage.classList.add('normal');
			} else {
				currentTestCoverage.classList.add('bad');
			}

			var averageTestCoverage = document.getElementById('averageTestCoverage');
			
			var average = Math.floor(sum / data.length);
			averageTestCoverage.innerText = average + "%";
			if (average >= 80) {
				averageTestCoverage.classList.add('success');
			} else if (average >= 60 && average < 80) {
				averageTestCoverage.classList.add('normal');
			} else {
				averageTestCoverage.classList.add('bad');
			}

		},
		error: (error) => console.error(e)
	})

}

function addTaskStatuses() {

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/jiratasks/ByStatus/1/11",
		success: (data) => {

			var canvas = document.getElementById("taskStatuses").getContext('2d');

			var config = {
				type: 'bar',
				data: {
					labels: [],
					datasets: [{
						label: 'Задачи',
						data: [],
						backgroundColor: "#c9daf8"
					}]
				},
				options: {
					legend: {
						display: false
					},
					scales: {
						yAxes: [{
						  ticks: {
							beginAtZero: true
						  }
						}],
						
					},
				}
			};

			for (var i = 0; i < data.length; i++) {
				config.data.labels.push(data[i].state);
				config.data.datasets[0].data.push(data[i].count);
			}


			new Chart(canvas, config);

		},
		error: (error) => console.error(e)
	
	});
}

function addBugsAndFeatures() {

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/jiratasks/ByType/1",
		success: (data) => {

			var labels = [""];

			var datasetsBugsFeaturePercent = [{
				label: 'Отношение количества дефектов к количеству фич',
				data: [],
				backgroundColor: "blue",
				borderColor: "blue",
				fill: false
			}];
		
			var datasetsBugsFeatureSP = [{
				label: 'Отношение количества дефектов к количеству фич',
				data: [],
				backgroundColor: "blue",
				borderColor: "blue",
				fill: false
			}]

			for (var i = 0; i < data.length; i++) {
				var number = i+1;
				labels.push('Sprint ' + (i+1));
				datasetsBugsFeaturePercent[0].data.push(data[i].defectInPercent);
				datasetsBugsFeatureSP[0].data.push(data[i].defectsInStoryPoint);
			}

			addLine('bugsAndFeaturesPercent', labels, datasetsBugsFeaturePercent, '%', '', false);
			addLine('bugsAndFeaturesSP', labels, datasetsBugsFeatureSP, 'SP', '', false);
		},
		error: (error) => console.error(e)
	})

	


}

function addCriticalBugsRepair() {
	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/jiratasks/CriticalBugResolve",
		success: (data) => {
			var labels = [];
			var datasetsCriticalBugsRepair = [{
				label: 'Время на исправление критичных дефектов',
				data: [],
				backgroundColor: "blue",
				borderColor: "blue",
				fill: false
			}];

			for (var i = 0; i < data.length; i++) {
				labels.push("Sprint " + data[i].sprintId)
				datasetsCriticalBugsRepair[0].data.push(data[i].days)
			}

			addLine('criticalBugsRepair', labels, datasetsCriticalBugsRepair, 'Days', '', false,  5, 20);
		},

		error: (error) => console.error(e)
	})
}

function addTimeToMergePR() {

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/PoolRequests/confirmationtime",
		success: (data) => {

			var canvas = document.getElementById("timeToMergePR").getContext('2d');

			var config = {
				type: 'bar',
				data: {
					labels: ["4h", "8h", "16h", "More"],
					datasets: [{
						label: 'Количество',
						data: [],
						backgroundColor: ["green", "#ffe599", "orange", "red"]
					}]
				},
				options: {
					legend: {
						display: false
					},
					scales: {
						yAxes: [{
						  ticks: {
							beginAtZero: true
						  }
						}],
					}
				}
			};

			for (var i = 0; i < data.length; i++) {
				config.data.datasets[0].data.push(data[i]);
			}


			new Chart(canvas, config);

		},
		error: (error) => console.error(e)
	
	});

}

function addCeremoniesAverageTime() {

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/scrum/CeremonialTiming/1/14",
		success: (data) => {

			var labels = [];
			var datasetsCeremonies = [{
				data: [],
				backgroundColor: "blue",
				borderColor: "blue",
				fill: false
			}];

			for (var i = 0; i < data.length; i++) {
				labels.push(data[i]);
				datasetsCeremonies[0].data.push(data[i])
			}

			addLine('ceremoniesAverageTime', labels, datasetsCeremonies, 'Minutes', 'Days', false, 2, 20);
		},

		error: (error) => console.error(e)
	})
}

function addCO2() {

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/Fun/co2/1",
		success: (data) => {
			var co2 = document.getElementById('CO2');
			co2.innerText = data + " ppm";

			if (data < 700) {
				co2.classList.add("success");
			} else if (data >= 700 && data < 800) {
				co2.classList.add("normal");
			} else if (data >= 800) {
				co2.classList.add("bad");
			}
		},
		error: (error) => console.error(e)
	})

}

function addTeamMood() {

	$.ajax({
		url: "http://172.30.14.84/FSO.SDD.NativeWebApi/api/scrum/CommandFeeling/1",
		success: (data) => {
			switch (data[0]) {
				case "Good":
					document.getElementById('teamMood').innerHTML = "&#128522";
					break;
				case "Normal":
					document.getElementById('teamMood').innerHTML = "&#128528";
					break;
				case "Bad": 
					document.getElementById('teamMood').innerHTML = "&#128545";
					break;
				default:
					break;
			}
		},
		error: (error) => console.error(e)
	})
	
	// &#128522 - lucky
	// &#128528 - usual
	// &#128545 - angry

	

}

function addLine(id, labelsArray, datasetsArray, yTitle, xTitle, displayLegend, step = 5, maxV = 100) {
	var canvas = document.getElementById(id).getContext('2d');

	var config = {
		type: 'line',
		data: {
			labels: labelsArray,
			datasets: datasetsArray
		},
		options: {
			scales: {
				yAxes: [{
				  scaleLabel: {
					display: true,
					labelString: yTitle
				  },
				  ticks: {
					beginAtZero: true,
					steps: 10,
					stepValue: step,
					max: maxV
					}
				}],
				xAxes: [{
					scaleLabel: {
					  display: true,
					  labelString: xTitle
					}
				}]
			},
			legend: {
				display: displayLegend
			}
		}
	}

	new Chart(canvas, config);
}

function addPieProgress(id, value, label) {
	var canvas = document.getElementById(id).getContext('2d');

	var config = {
		type: 'pie',
		data: {
			labels: [label, ""],
			datasets: [{
				backgroundColor: [ "red", "lightGray" ],
				data: [value, 100]
			}]
		},
		options: {
			legend: {
				display: false
			}
		}
	}

	new Chart(canvas, config);
}

function addLight(id, status, value, label) {

	var color, label;

	switch (status) {
		case 'Bad':
			color = "#f4cccc";
			break;
		case 'Normal':
			color = "#ffe599";
			break;
		case 'Success':
			color = "#d9ead3";
			if (value == 0) value = 100;
			break;
		default:
			color = "lightGray";
			break;
	}
	
	let config = {
		type: 'pie',
		data: {
			datasets: [{
				data: [value],
				backgroundColor: [color],
			}],
			labels: [label]
		},
		options: {
			responsive: true,
			legend: {
				display: false
			}
		}
	};


	let canvas = document.getElementById(id).getContext('2d');
	new Chart(canvas, config);
}

function secondChart() {

}


