using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using TODOGraphQL.Domain.DataTypes.Common;

namespace TODOGraphQL.Persistence.ReactiveExtensions
{
    public class GenericObservable<TRequest, TResponse> : IObservable<KeyValuePair<Id, TResponse>>, IRequestPostProcessor<TRequest, IDictionary<Id, TResponse>>
        where TRequest : IRequest<IDictionary<Id, TResponse>>
    {
        private readonly ISubject<KeyValuePair<Id, TResponse>> _stream;

        public GenericObservable(ISubject<KeyValuePair<Id, TResponse>> stream)
        {
            _stream = stream;
        }

        public Task Process(TRequest request, IDictionary<Id, TResponse> response)
        {
            foreach (var item in response)
            {
                _stream.OnNext(item);
            }
            return Task.CompletedTask;
        }

        public IDisposable Subscribe(IObserver<KeyValuePair<Id, TResponse>> observer) 
            => _stream.Subscribe(observer);
    }
}