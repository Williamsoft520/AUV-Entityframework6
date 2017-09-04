## AUV-Entityframework6
It is a provider for AUV using entityframework 6.1.3.

AUV 为 entityframework 6.1.3 版本的提供器。

## Install from Nuget 从 Nuget 进行安装
> Install-Package AUV.Entityframework6

## Lastest Version v2.5

* Support Framework 4.5+
* [new]Query predicate argument.

        IQuerable<TEntity> query = _repository.Query(m => m.Name == name);

* [new]Remove entity by id extension

        _repository.Remove(id);

* [new]Remove multi entities or multi id extension

        _repository.RemoveByRange(entity1,entity2,entity3);
        _repository,RemoveByRange(id1,id2,id3);
        
* [new]QueryNoTracking extension

        _repository.QueryNoTracking();
        _repository.QueryNoTracking(m => m.Name == name);
        
        

## How to use?
**Read [Document](https://github.com/Williamsoft520/AUV/wiki)** 



## Other Provider
* AUV.PetaPoco5