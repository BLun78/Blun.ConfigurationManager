using System;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Blun.ConfigurationManager.ServiceModel.Proxy
{
    public interface IProxyInstanceFactory
    {
        NetworkCredential ClientCredential { get; set; }
        /// <summary>
        /// Die Methode übernimmt das korrekte Handling des WCF-Proxyaufrufs.
        /// Es wird dafür gesorgt, dass die Verbindung korrekt aufgebaut und wieder abgebaut wird ohne das Memoryleaks entstehen
        /// 
        /// Die dabei auftretenden Fehler werden wie gehabt weitergeleitet. 
        /// Es wird nur der WCF-Proxy wie im Pattern definiert korrekt geschlossen.
        /// </summary>
        /// <typeparam name="TProxy">Generierter Serviceclient</typeparam>
        /// <typeparam name="TProxyInterface">Das ClientBase Interface von InkassoServiceClient z.B. InkassoService.</typeparam>
        /// <param name="proxyMethod">Methode die auf dem Proxy aufgerufen werden soll.</param>
        /// <param name="endpointConfigurationName">Client-Endpunktname aus der Web.config</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "[Bjoern lundstroem, 07.04.2015] Das 'TProxyInterface' wird nur als Definition verwendet. Es wird der Proxy automatisch neu erstellt. Dies ist die aufgabe dieses WCF Patterns."),
         SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "[Raphael Herding, 06.11.2014] Der WCF-Client wird nur einmal geschlossen.")]
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "[Raphael Herding, 06.11.2014] Der WCF-Client wird auf jeden Fall geschlossen.")]
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "[Raphael Herding, 06.11.2014] Hier sollen alle Exception-Messages in dem ErrorLabel angezeigt werden.")]
        Task<bool> InvokeWcfRequest<TProxy, TProxyInterface>(Func<TProxy,
                                                            Task<bool>> proxyMethod,
                                                            string endpointConfigurationName)
            where TProxyInterface : class
            where TProxy : Blun.ConfigurationManager.ServiceModel.ClientBase<TProxyInterface>;
    }
}