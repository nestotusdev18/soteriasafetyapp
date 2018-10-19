var dataInit = [   
];
const DATE_OPTIONS = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
var  SCHOOL_NAME =  $('#hdnSchoolName').val();


var DashboardApp = React.createClass({

	getInitialState: function() {
        return {           
            current_data: {  },
        };
    },
	componentDidMount: function() {
		var self = this;
		$.ajax({
			url: this.props.url,
			dataType: 'json',
			success: function(data) {
				this.setState({
					current_data: data,
				});
			}.bind(this),
			error: function(xhr, status, err) {
				console.error(this.props.url, status, err.toString());
			}.bind(this)
		});

		//SignalR Code
		var vhub = $.connection.soteriaHub;
		vhub.client.showLiveResult = function(data) {
			var obj = $.parseJSON(data);
			self.setState({
				current_data: obj
			});
		};
		$.connection.hub.start();

	},
	render: function() {
		  
		 var boysprofile = Object.values(this.state.current_data).map((value) => {
			 return (	
						   <div className="row">
						   
						   {(() => {
											if (value.RoomType==2) {
											  return (
						   
						  <div className="col-md-12">
							 <div className="card">
								<div className="cardcontainer">
								   <div className="row">
									  
										<div className="col-md-12 cardpad">

											
													<div className="col-md-2 imgdiv"  style={{background: value.IsOffline ? '#b2b5b4' : (value.LongRecent  >0 || value.LongStay  >0 || value.WrongPersonRecent  >0 || value.WrongPerson >0) ? '#e94d66' : '#30bea1'}}>
														<img src="../content/images/boy-icon.png"/> 
													</div>
											
											
											  <div className="col-md-3" >
												 <br/>
												 <h4>{value.FloorName}</h4>
												 <h5>UNIQUE: {value.UniqueId}</h5>
											  </div>
											  <div className="col-md-2">
												 <div className="">
													<h1 align="center" className={"" + (value.IsOffline ? 'grey' : 'total' )}>{value.CurrentCount}</h1>
													<h5 align="center">Current</h5>
												 </div>
											  </div>
											  <div className="col-md-2">
												 <div className="vl" align="center">
													<h1 align="center" className={"" + (value.IsOffline ? 'grey' : (value.LongRecent  >0 || value.LongStay  >0) ? 'wrong' : 'green')} >
														
														
														{(() => {
															if (value.LongStay>0 || value.LongRecent>0) {
															  return (
																	<span>{value.LongRecent}<sub>+{value.LongStay}</sub></span>
																)
															}
															else  {
															  return (
																	<span>--</span>
																)
															}															
														})()}
														
													
														
													
													</h1>
													<h5 align="center" className="long">Long Stay</h5>
												 </div>
											  </div>
											  <div className="col-md-3">
												 <div className="vl" align="center">
													<h1 align="center" className={"" + (value.IsOffline ? 'grey' : (value.WrongPerson>0 || value.WrongPersonRecent>0) ? 'wrong' : 'green')} >
													
															{(() => {
															if (value.WrongPerson>0 || value.WrongPersonRecent>0) {
															  return (
																	<span>{value.WrongPersonRecent}<sub>+{value.WrongPerson}</sub></span>
																)
															}
															else  {
															  return (
																	<span>--</span>
																)
															}															
														})()}
													
													</h1>
													<h5 align="center">Wrong Person</h5>
												 </div>
											  </div>
											
								   </div>
								</div>
							 </div>
						  </div>
						</div>
						)
											}

												
											
											})()}
						</div>
			)
		 });
		 
 var girlsprofile = Object.values(this.state.current_data).map((value) => {
			 return (	
						   <div className="row">
						   
						   {(() => {
											if (value.RoomType==3) {
											  return (
						  <div className="col-md-12">
							 <div className="card">
								<div className="cardcontainer">
								   <div className="row">
										<div className="col-md-12 cardpadgl">
											  <div className="col-md-3" >
												 <br/>
												 <h4 style={{margin: '10px 10px 0px -40px'}}>{value.FloorName}</h4>
												 <h5 style={{margin: '10px 10px 0px -40px'}}>UNIQUE: {value.UniqueId}</h5>
											  </div>
											  <div className="col-md-2">
												 <div className="">
													<h1 align="center" className={"" + (value.IsOffline ? 'grey' : 'total' )}>{value.CurrentCount}</h1>
													<h5 align="center">Current</h5>
												 </div>
											  </div>
											  <div className="col-md-2">
												 <div className="vl" align="center">
													<h1 align="center" className={"" + (value.IsOffline ? 'grey' : (value.LongRecent  >0 || value.LongStay  >0) ? 'wrong' : 'green')} >	
														
														{(() => {
															if (value.LongStay>0 || value.LongRecent>0) {
															  return (
																	<span>{value.LongRecent}<sub>+{value.LongStay}</sub></span>
																)
															}
															else  {
															  return (
																	<span>--</span>
																)
															}															
														})()}
														
													</h1>
													<h5 align="center" className="long">Long Stay</h5>
												 </div>
											  </div>
											  <div className="col-md-3">
												 <div className="vl" align="center">
													<h1 align="center" className={"" + (value.IsOffline ? 'grey' : (value.WrongPerson>0 || value.WrongPersonRecent>0) ? 'wrong' : 'green')} >	
															{(() => {
															if (value.WrongPerson>0 || value.WrongPersonRecent>0) {
															  return (
																	<span>{value.WrongPersonRecent}<sub>+{value.WrongPerson}</sub></span>
																)
															}
															else  {
															  return (
																	<span>--</span>
																)
															}															
														})()}
													
													</h1>
													<h5 align="center">Wrong Person</h5>
												 </div>
											  </div>
											  
											 <div className="col-md-2 imgdiv" style={{background: value.IsOffline ? '#b2b5b4' : (value.LongRecent  >0 || value.LongStay  >0 || value.WrongPersonRecent  >0 || value.WrongPerson >0) ? '#e94d66' : '#30bea1'}}>
												<img src="../content/images/girl-icon.png"/>
											</div>
											
								   </div>
								</div>
							 </div>
						  </div>
						</div>
						)
											} 
											})()}
						</div>
			)
			
			
		 });
		 
		return(
		<div className="DashboardApp">
			<div className="row " >
				<div className="col-md-1 " ></div>
				<div className="col-md-10 " >
					<div className="col-md-12 card"  >
						<h2  style={{textAlign:"center"}}>{SCHOOL_NAME}</h2>
						<h3 style={{textAlign:"center"}}>
						{(new Date()).toLocaleDateString('en-US', DATE_OPTIONS)}
						</h3>
					</div>
				</div>
				<div className="col-md-1" ></div>
			</div>
			<div className="row " >
				 <div className="col-md-1 " ></div>
				  <div className="col-md-10 " >
					 <div className="col-md-12 card" >
						<div className="row">
							<div className="col-md-6">
								{boysprofile}
							</div>
							<div className="col-md-6">
								{girlsprofile}
							</div>
						  </div>
						  <div className="row">  <div className="col-md-12 " ><br/></div></div>
					 </div>
				  </div>
				  <div className="col-md-1 " ></div>
			</div>
		</div>
        );
    }

});



React.render(
    <DashboardApp url="/home/datacount" />,
    document.getElementById('container')
);