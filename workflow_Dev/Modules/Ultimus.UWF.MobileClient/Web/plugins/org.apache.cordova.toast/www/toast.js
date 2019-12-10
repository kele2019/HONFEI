cordova.define("org.apache.cordova.toast.toast", function(require, exports, module) { var cordova=require('cordova');

var Toast =function(){

	Toast.prototype.toast=function(success,error,str){

		cordova.exec(sucess,error,'ToastPlugin','sampletoast',str)
		//'Toast'对应我们在java文件中定义的类名，
		//'sampletoast'是在这个类中调用的自定义方法，也就是action
		//str是我们客户端传递给这个方法的参数，是个数组

	}

}
var toast=new Toast();

module.exports=toast;

});
