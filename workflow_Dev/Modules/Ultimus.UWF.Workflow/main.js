// JavaScript Document

var openFlg;
// 自动加载
function loadFun() { 
	var array = document.getElementById('leftMenu').childNodes; 
	for (var i = 0; i < array.length; i++) { 
		var childnodes = array[i].childNodes; 
		for (var j = 0; j < childnodes.length; j++) {
			if (childnodes[j].tagName == "UL") { 
			childnodes[j].style.display = "none"; 
			} 
		} 
	}
	var table1 = document.getElementById("infoTab");  
	var rows = table1.getElementsByTagName("tr");  
	for (var i = 1; i < rows.length-1; i++) {
		rows[i].index = i;  
		rows[i].className = (i % 2 == 0) ? "oddrow" : "evenrow";  
	} 
	// 导航栏初期状态值设定
	openFlg = 1;
}

// 分类按钮动作
function circuitSel(liId){
	var allLi = liId.parentNode.childNodes;
	for(var i=0;i<allLi.length;i++){
		
		if (allLi[i].tagName == 'LI'){
			allLi[i].style.color = '#626262';
			allLi[i].childNodes[0].src = "images/" + allLi[i].id + ".png";
		}		
	}	
	liId.childNodes[0].src = "images/" + liId.id + "Sel.png";
	liId.style.color = "#008dde";
	var msgIcon = document.getElementById(liId.id+"Icon");
	var msgNum = document.getElementById(liId.id+"Num");
	msgIcon.style.display = "none";
	msgNum.style.display = "none";
	
}

// 排序按钮选择动作
function circuitSel1(liId){
	var allLi = liId.parentNode.childNodes;
	for(var i=0;i<allLi.length;i++){
		if (allLi[i].tagName == 'LI'){
			allLi[i].className = 'normalLi1';
			allLi[i].style.color = '#626262';
			allLi[i].style.height = '25px';
			allLi[i].style.lineHeight = '25px';
			allLi[i].style.marginTop = '7px';
			allLi[i].childNodes[0].childNodes[0].src = "images/" + allLi[i].id + ".png";
		}
	}
	liId.className = 'selectLi1';
	liId.style.color = "#008dde";
	liId.style.height = '39px';
	liId.style.lineHeight = '39px';
	liId.style.marginTop = '0px';
	liId.childNodes[0].childNodes[0].src = "images/" + liId.id + "Sel.png";
}

// 全选checkBtn
function allSel(){
	var allCheck = document.getElementById('allCheck');
	var all = document.getElementsByTagName('input');
	if (allCheck.checked) {
		for(var i=0;i<all.length;i++){
			all[i].checked = true;
		}
	} else {
		for(var i=0;i<all.length;i++){
			all[i].checked = false;
		}
	}
	btnTrue();
}

// 导航栏一级菜单选择
function openSub(parentLi){
	
	// 展开导航栏	
	if(openFlg == 0) {
		changeNavigate();
	}	
	var parentNode = parentLi.parentNode.childNodes;
	for(var i=0;i<parentNode.length;i++){
		if (parentNode[i].tagName == 'LI') {
			if (parentLi != parentNode[i]) {
				parentNode[i].style.backgroundColor="#f6f6f6";
				parentNode[i].style.color="#626262";
				
				var subNode =  parentNode[i].childNodes;
				for(var j=0;j<subNode.length;j++){
					if (subNode[j].className == 'stress') {
						subNode[j].style.backgroundColor="#f6f6f6";
						subNode[j].childNodes[0].src =  "images/" + parentNode[i].id + ".png";
					}
					if (subNode[j].tagName == 'IMG') {
						if (subNode[j].className == 'puDownIcon'){
							subNode[j].src =  "images/open.png";
						}
						
					}
					if (subNode[j].tagName == 'UL') {
						subNode[j].style.display='none';
											
						var subLi =  subNode[j].childNodes;
						for(var m=0;m<subLi.length;m++){
							if (subLi[m].tagName == 'LI') {
								subLi[m].style.backgroundColor="#f9f9f9";
								subLi[m].style.color="#696969";
							}
						}
					}
				}
			}			
		}		
	}
	parentLi.style.backgroundColor = "#009ffb";
	parentLi.style.color="#ffffff";
	
	
	var subLi = parentLi.childNodes;
	for(var a=0;a<subLi.length;a++){
		if (subLi[a].className == 'stress') {
			subLi[a].style.backgroundColor="#008ddd";
			subLi[a].childNodes[0].src =  "images/" + parentLi.id + "Sel.png";
		}	
		if (subLi[a].tagName == 'IMG') {
			if (subLi[a].className == 'puDownIcon'){
				subLi[a].src =  "images/puDown.png";
			}
			
		} else if (subLi[a].tagName == 'UL') {
			subLi[a].style.display='block';
			subLi[a].style.backgroundColor='#f9f9f9';
			subLi[a].style.color="#696969";
		}
		
		var menuSelName = document.getElementById('menuSel');
		if (subLi[a].nodeValue != null && subLi[a].nodeValue.trim() != ''){
			menuSelName.innerHTML = subLi[a].nodeValue;
		}
	}
	var selMenu = document.getElementById('selMenu');
	selMenu.src = "images/" +"" + parentLi.id + ".png";
	
}



// 导航栏二级菜单选择
function selSub(subNode){
	var parentNode = subNode.parentNode.childNodes;
	for(var i=0;i<parentNode.length;i++){
		if (parentNode[i].tagName == 'LI') {
			parentNode[i].style.backgroundColor="#f9f9f9";
			parentNode[i].style.color="#696969";
		}
	}
	subNode.style.backgroundColor="#009ffb";
	subNode.style.color="#ffffff";	
	
	var subMenuSelName = document.getElementById('subMenuSel');
	if (subNode.childNodes[0].nodeValue != null && 
		subNode.childNodes[0].nodeValue.trim() != ''){
		subMenuSelName.innerHTML = subNode.childNodes[0].nodeValue;
	}
	
	
}

//页面上部下拉单显示
function pudownInfo(){
	var suspensionDiv = document.getElementById('suspension');
	var otherColorDiv = document.getElementById('otherColor');
	if (suspensionDiv.style.display == 'block') {
		suspensionDiv.style.display = 'none';
		otherColorDiv.style.backgroundColor = '#00397c';
	} else {
		suspensionDiv.style.display = 'block';
		otherColorDiv.style.backgroundColor = '#002f67';
	}
}

// 整个文档的点击事件 --- 页面上部下拉单隐藏
document.onclick = function (event) { 
	var e = event || window.event;  
	var elem = e.srcElement||e.target;
	
	while(elem){
		if(elem.id == "suspension" || elem.id == "hederInfo") {  
			return;  
		}  
		elem = elem.parentNode;       
	} 
//	var suspensionDiv = document.getElementById('suspension');
//	suspensionDiv.style.display = 'none';
//	var otherColorDiv = document.getElementById('otherColor');
//	otherColorDiv.style.backgroundColor = '#00397c';
} 


// 改变导航栏宽度
function changeNavigate(){
	var rightDiv = document.getElementById('right');
	var leftDiv = document.getElementById('left');
	var closeBtn = document.getElementById('halfClose');
	if (openFlg == 1) {
		rightDiv.style.marginLeft = '44px';
		closeBtn.style.marginLeft = '32px';
		leftDiv.style.width = '44px';
		openFlg = 0;
	} else {
		rightDiv.style.marginLeft = '150px';
		closeBtn.style.marginLeft = '138px';
		leftDiv.style.width = '150px';
		openFlg = 1;
	}	
}

String.prototype.trim=function(){
	return this.replace(/(^\s*)|(\s*$)/g, "");
}
//指派、收回指派活性
function btnTrue(){
	var allCheckFlg = true;
	var oneSelFlg = false;
	var allCheck = document.getElementById('allCheck');
	var all = document.getElementsByTagName('input');
	var assignBtn = document.getElementById('assign');
	var assignBackBtn = document.getElementById('assignBack');
	
	
	
	
	for(var i=0;i<all.length;i++){
		if (all[i].checked == false){
			allCheckFlg = false;
		} else {
			oneSelFlg = true;
		}
	}
	var allNuSel = true;
	for(var i=0;i<all.length;i++){
		if (all[i].checked == true){
			allNuSel = false;
		}
	}
	if (!allCheckFlg) {
		allCheck.checked = false;
	}
	if (oneSelFlg) {
		assignBtn.disabled = false;
		assignBackBtn.disabled = false;
		assignBtn.className = 'selBtn';
		assignBackBtn.className = 'selBtn';
	} 
	if (allNuSel) {
		assignBtn.disabled = true;
		assignBackBtn.disabled = true;
		assignBtn.className = 'unSelBtn';
		assignBackBtn.className = 'unSelBtn';
	}
}
//翻页
function goPage(pageNum){
	var firstPageBtn = document.getElementById('firstPage');
	var pretPageBtn = document.getElementById('pretPage');
	var allA = pageRight.getElementsByTagName('A');
	if (pageNum == 1) {
		firstPageBtn.disabled = true;
		pretPageBtn.disabled = true;		
		firstPageBtn.style.color = '#aaa';
		pretPageBtn.style.color = '#aaa';
	} else {
		firstPageBtn.disabled = false;
		pretPageBtn.disabled = false;
		firstPageBtn.style.color = '#626262';
		pretPageBtn.style.color = '#626262';
	}
	for (var i=0; i<allA.length; i++){
		if (pageNum == i + 1) {
			allA[i].style.color = '#009ffb';
		} else {
			allA[i].style.color = '#626262';
		}
	}
}