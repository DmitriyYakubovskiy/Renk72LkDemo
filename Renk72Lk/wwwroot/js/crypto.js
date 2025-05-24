			const app = angular.module('App', []);

			app.directive('customOnChange', () => {
				return {
					restrict: 'A',
					link: (scope, element, attrs) => {
						const onChangeHandler = scope.$eval(attrs.customOnChange);
						element.on('change', onChangeHandler);
						element.on('$destroy', () => {
							element.off();
						});
					}
				};
			});

			app.controller('Crypto', ($scope) => {
				$scope.certs = [];
				$scope.selectedCert = null;
				$scope.signInfo = null;
				$scope.data = 'Test data';
				$scope.files = null;
				$scope.sign = '';
				$scope.pluginReady = false;

				let crypto = new CryptoHelper();

				crypto.init().then(() => {
					$scope.pluginReady = true;

					crypto.getCertificates().then((certs) => {
						$scope.certs = certs;
						$scope.$digest();
					});
				});
				
				$scope.doSign = (data, download = false) => {
					crypto
						.sign($scope.selectedCert.$original, data)
						.then((signMessage) => {
							$scope.sign = signMessage;

							if (download) {
								if (signMessage instanceof Array) {
									signMessage.forEach((sign, i) => {
										downloadData(sign, `${$scope.files[i].name}.sig`, 'text/plain');
									});
								} else {
									downloadData(signMessage, `signature.sig`, 'text/plain');
								}
							}

							$scope.$digest();
						});
				};

				$scope.doVerify = () => {
					crypto
						.verify($scope.data, $scope.sign, true)
						.then((verifyResult) => {
							if (verifyResult) {
								$scope.signInfo = verifyResult;
								$scope.$digest();
							}
						});
				};

				$scope.openFile = ($event) => {
					$scope.files = $event.target.files;
					$scope.$digest();
				};

				function downloadData(data, name, type) {
					const file = new Blob([data], { type });
					const link = document.createElement('a');
					link.href = URL.createObjectURL(file);
					link.download = name;
					document.body.appendChild(link);
					link.click();
					link.remove();
				}

			});