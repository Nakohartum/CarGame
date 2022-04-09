using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

using Object = UnityEngine.Object;

internal abstract class BaseController : IDisposable
{
    private List<GameObject> _gameObjects;
    private List<IDisposable> _disposables;
    private bool _isDisposed;


    public void Dispose()
    {
        if (_isDisposed)
            return;
        OnDispose();

        _isDisposed = true;

        DisposeDisposables();
        DisposeGameObjects();

    }

    private void DisposeDisposables()
    {
        if (_disposables == null)
            return;

        foreach (IDisposable disposable in _disposables)
            disposable.Dispose();

        _disposables.Clear();
    }

    private void DisposeGameObjects()
    {
        if (_gameObjects == null)
            return;

        foreach (GameObject gameObject in _gameObjects)
            Object.Destroy(gameObject);

        _gameObjects.Clear();
    }

    protected virtual void OnDispose() { }


    protected void AddController(BaseController baseController)
    {
        _disposables ??= new List<IDisposable>();
        _disposables.Add(baseController);
    }

    protected void AddRepository(IRepository repository)
    {
        _disposables ??= new List<IDisposable>();
        _disposables.Add(repository);
    }
    
    protected void AddGameObject(GameObject gameObject)
    {
        _gameObjects ??= new List<GameObject>();
        _gameObjects.Add(gameObject);
    }
}
