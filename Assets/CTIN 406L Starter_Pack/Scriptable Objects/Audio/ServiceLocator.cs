using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : Singleton<ServiceLocator> {

    private IDictionary<Type, MonoBehaviour> serviceReferences;
    protected void Awake() {
        SingletonBuilder( this );
        serviceReferences = new Dictionary<Type, MonoBehaviour>();
    }

    public T GetService<T>() where T : MonoBehaviour, new() {
        UnityEngine.Assertions.Assert.IsNotNull( serviceReferences,
	        "Someone has requested a service prior to the locator's intialization." );

        bool serviceLocated = serviceReferences.ContainsKey( typeof( T ) );
        if ( !serviceLocated ) {
            serviceReferences.Add( typeof( T ), FindObjectOfType<T>() );
        }

        UnityEngine.Assertions.Assert.IsTrue( serviceReferences.ContainsKey( typeof( T ) ),
	        "Could not find service: " + typeof( T ) );
        var service = ( T ) serviceReferences [ typeof( T ) ];
        UnityEngine.Assertions.Assert.IsNotNull( service,
	        typeof( T ).ToString() + " could not be found." );
        return service;
    }
}
