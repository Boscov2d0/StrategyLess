using Abstractions.Commands;
using Core;
using System;

namespace UserControllSystem.UI.Model.CommandCreator
{
    public abstract class CommandCreatorBase<T> where T : ICommand
    {
        public ICommandExecutor ProcessCommandExecutor(ICommandExecutor commandExecutor, Action<T> callback)
        {
            ICommandExecutor<T> classSpecificExecutor = commandExecutor as ICommandExecutor<T>;
            if (classSpecificExecutor != null)
            {
                ClassSpecificCommandCreation(callback);
            }
            return commandExecutor;
        }
        protected abstract void ClassSpecificCommandCreation(Action<T> creationCallback);
        public virtual void ProcessCancel() { }
    }
}