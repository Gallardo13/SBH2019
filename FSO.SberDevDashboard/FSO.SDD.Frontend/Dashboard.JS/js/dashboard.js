$(function(){ //DOM Ready
	
	//instantiate gridster 
	$(".gridster > ul").gridster({
		widget_margins: [8, 8],
		widget_base_dimensions: [95, 50],
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

	// static data

	addPieProgress("unplannedWorkPercent", 30, '%')
	addPieProgress("unplannedWorkStoryPoints", 17, 'SP')


	addLight('branch1', 'Success', 1, 'Success');
	addLight('branch2', 'Success', 1, 'Success');
	addLight('branch3', 'Success', 1, 'Success');
	addLight('branch4', 'Bad', 1, 'Failure');

	addSonarMetrics();

	addTaskStatuses();

	//addUnclosedBugsSquadGartner();

	addBugsAndFeatures();

	addCriticalBugsRepair();

	addTimeToMergePR();

	addCeremoniesAverageTime();

	addTeamMood();

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

			var labels = [];
			for (var i = 0; i < data.length; i++) {
				labels.push('Релиз ' + data[i].releaseID);
				datasetsDebt[0].data.push(data[i].percent);
			}

			addLine('sonarTechnicalDebt', labels, datasetsDebt, '%', 'Релизы', false);
		},
		error: (error) => console.error(e)
	})

	

	var labels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct"];
	

	var datasetsTests = [{
		label: 'Покрытие тестами',
		data: [21, 25, 40, 45, 45, 51, 60, 71, 73, 73],
		backgroundColor: "blue",
		borderColor: "blue",
		fill: false
	}]

	
	addLine('sonarTestCoverage', labels, datasetsTests, '%', 'Months', false);
}

function addTaskStatuses() {
	var canvas = document.getElementById("taskStatuses").getContext('2d');
	new Chart(canvas, {
		type: 'bar',
		data: {
			labels: ["Создана", "Запланирована", "Открыта", "В работе", "Завершена", "Отклонена"],
			datasets: [{
				label: 'Задачи',
				data: [12, 20, 3, 17, 30, 24, 7],
				backgroundColor: "blue"
			}]
		},
		options: {
			legend: {
				display: false
			}
		}
	});
}

function addBugsAndFeatures() {
	var labels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct"];
	var datasetsBugsFeaturePercent = [{
		label: 'Отношение количества дефектов к количеству фич',
		data: [11, 9, 5, 13, 5, 4, 5, 8, 3, 2],
		backgroundColor: "blue",
		borderColor: "blue",
		fill: false
	}];

	var datasetsBugsFeatureSP = [{
		label: 'Отношение количества дефектов к количеству фич',
		data: [0.123, 0.093, 0.193, 0.185, 0.092, 0.050, 0.063, 0.099, 0.102, 0.089],
		backgroundColor: "blue",
		borderColor: "blue",
		fill: false
	}]

	addLine('bugsAndFeaturesPercent', labels, datasetsBugsFeaturePercent, '%', 'Months', false);
	addLine('bugsAndFeaturesSP', labels, datasetsBugsFeatureSP, 'SP', 'Months', false, 0.05, 1);
}

function addCriticalBugsRepair() {
	var labels = ["Sprint 1", "Sprint 2", "Sprint 3", "Sprint 4", "Sprint 5", "Sprint 6"];
	var datasetsCriticalBugsRepair = [{
		label: 'Время на исправление критичных дефектов',
		data: [20, 35, 30, 25, 15, 15],
		backgroundColor: "blue",
		borderColor: "blue",
		fill: false
	}];

	addLine('criticalBugsRepair', labels, datasetsCriticalBugsRepair, 'Days', '', false,  5, 50);
}

function addTimeToMergePR() {
	var canvas = document.getElementById("timeToMergePR").getContext('2d');
	var bar = new Chart(canvas, {
		type: 'bar',
		data: {
			labels: ["4h", "8h", "16h", "More"],
			datasets: [{
				label: 'Количество',
				data: [23, 15, 10, 3],
				backgroundColor: ["green", "#ffe599", "orange", "red"]
			}]
		},
		options: {
			legend: {
				display: false
			}
		}
	});
}

function addCeremoniesAverageTime() {
	var canvas = document.getElementById("ceremoniesAverageTime").getContext('2d');
	new Chart(canvas, {
		type: 'bar',
		data: {
			labels: ["Daily", "Retro", "Planning", "Review"],
			datasets: [{
				label: 'Минуты',
				data: [28, 63, 125, 48],
				backgroundColor: "#d9ead3"
			}]
		},
		options: {
			legend: {
				
			}
		}
	});
}

function addTeamMood() {
	var mood = Date.now() % 3;
	switch (mood) {
		case 0:
			document.getElementById('teamMood').innerHTML = "&#128522";
			break;
		case 1:
			document.getElementById('teamMood').innerHTML = "&#128528";
			break;
		case 2: 
			document.getElementById('teamMood').innerHTML = "&#128545";
			break;
		default:
			break;
	}
	
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


