# Elasticsearch Query Örnekleri ve Açıklamaları

Bu doküman, Elasticsearch'te kullanılan farklı sorgu türlerini örneklerle açıklamaktadır.

## Term Query
Belirli bir terimle tam eşleşen belgeleri döndürür. `keyword` türündeki alanlarla kullanılmalıdır.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "term": {
      "customer_first_name.keyword": {
        "value": "Sonya"
      }
    }
  }
}
```

## Terms Query
Belirli birden fazla değerden herhangi biriyle eşleşen belgeleri döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "terms": {
      "customer_id": [
        "45",
        "46",
        "14"
      ]
    }
  }
}
```

## IDs Query
Belirli bir veya daha fazla ID'ye sahip belgeleri döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "ids": {
      "values": ["...", "..."]
    }
  }
}
```

## Exists Query
Belirtilen alanın mevcut olduğu belgeleri döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "exists": {
      "field": "order_id"
    }
  }
}
```

## Prefix Query
Belirtilen ön ekle başlayan alanlarla eşleşen belgeleri döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "prefix": {
      "customer_full_name.keyword": {
        "value": "Son"
      }
    }
  }
}
```

## Range Query
Bir alanın belirli bir aralıkta olduğu belgeleri döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "range": {
      "taxful_total_price": {
        "gte": 100,
        "lte": 200
      }
    }
  }
}
```

## Wildcard Query
Belirli bir desenle eşleşen belgeleri döndürür. `?` bir karakteri, `*` ise sıfır veya daha fazla karakteri ifade eder.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "wildcard": {
      "customer_full_name.keyword": {
        "value": "*Al*"
      }
    }
  }
}
```

## Fuzzy Query
Hatalı yazımlara toleranslı bir şekilde arama yapar. 
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "fuzzy": {
      "customer_first_name.keyword": {
        "value": "Sphanie"
      }
    }
  }
}
```

## Pagination (Sayfalama)
Sonuçları sayfalara bölmek için `from` ve `size` kullanılır.
```json
GET kibana_sample_data_ecommerce/_search
{
  "from": 0, 
  "size": 20,
  "query": {
    "fuzzy": {
      "customer_first_name.keyword": {
        "value": "Sphanie"
      }
    }
  }
}
```

## Includes
Yanıt içindeki sadece belirtilen alanları döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "_source": {
    "includes": ["customer_first_name", "customer_last_name", "category"]
  }, 
  "query": {
    "fuzzy": {
      "customer_first_name.keyword": {
        "value": "Sphanie"
      }
    }
  }
}
```

## Excludes
Yanıt içindeki belirtilen alanları hariç tutar.
```json
GET kibana_sample_data_ecommerce/_search
{
  "_source": {
    "excludes": ["category"]
  }, 
  "query": {
    "fuzzy": {
      "customer_first_name.keyword": {
        "value": "Sphanie"
      }
    }
  }
}
```

## Sort Query (Sıralama)
Sonuçları belirli bir alana göre sıralar.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "range": {
      "taxful_total_price": {
        "gte": 100,
        "lte": 500
      }
    }
  },
  "sort": [
    {
      "taxful_total_price": {
        "order": "desc"
      }
    }
  ]
}
```

## Full Text Queries
Tam metin sorguları, analiz edilmiş alanlar üzerinde arama yapmak için kullanılır. Örneğin, bir metin alanında anahtar kelime arama veya belirsiz eşleşmeleri yakalama işlemleri için idealdir.

### Match Query
Belirli bir metinle eşleşen belgeleri döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "match": {
      "customer_full_name": "Stephanie White"
    }
  }
}
```

### Match Phrase Query
Belirli bir ifadeyle (kelime sırasına dikkat ederek) eşleşen belgeleri döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "match_phrase": {
      "customer_full_name": "Stephanie White"
    }
  }
}
```

### Match Phrase Prefix Query
Belirli bir ifadenin başlangıcıyla eşleşen belgeleri döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "match_phrase_prefix": {
      "customer_full_name": "Steph"
    }
  }
}
```

### Multi Match Query
Birden fazla alanda tam metin araması yapar.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "multi_match": {
      "query": "Stephanie",
      "fields": ["customer_full_name", "customer_first_name"]
    }
  }
}
```

### Query String Query
Bir sorgu dizesini (query string) kullanarak karmaşık tam metin sorguları yapar.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "query_string": {
      "query": "(Stephanie OR Sonya) AND category:\"Men's Clothing\""
    }
  }
}
```

### Simple Query String Query
Kullanıcı dostu, basitleştirilmiş bir sorgu dizesi ile arama yapar.
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "simple_query_string": {
      "query": "+Stephanie -Sonya",
      "fields": ["customer_full_name", "customer_first_name"]
    }
  }
}
```

## Compound Query (Bileşik Sorgular)
Birden fazla sorguyu birleştirmek için kullanılır.

### Parametreler:
| Tür        | Zorunlu | Skor Etkisi | Amaç                           |
|------------|---------|-------------|--------------------------------|
| `must`     | Evet    | Evet        | Zorunlu koşul ekleme           |
| `should`   | Hayır   | Evet        | Opsiyonel koşul ekleme         |
| `must_not` | Evet    | Hayır       | Koşulları dışlama              |
| `filter`   | Evet    | Hayır       | Zorunlu koşul (skor etkisi yok)|


### Örnek:
```json
GET kibana_sample_data_ecommerce/_search
{
  "query": {
    "bool": {
      "must": [
        {
          "term": {
            "geoip.city_name": {
              "value": "New York"
            }
          }
        }
      ],
      "must_not": [
        {
          "range": {
            "taxful_total_price": {
              "lte": 100
            }
          }
        }
      ],
      "should": [
        {
          "term": {
            "category.keyword": {
              "value": "Women's Clothing"
            }
          }
        }
      ],
      "filter": [
        {
          "term": {
            "manufacturer.keyword": "Tigress Enterprises"
          }
        }
      ]
    }
  }
}
```

## Aggregations (Kümülasyonlar)
Veriyi gruplamak ve özetlemek için kullanılır.

### Bucket Aggregation
#### Term Aggregation
Alan değerlerini gruplar ve her grubun toplam sayısını döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "aggs": {
    "category_name": {
      "terms": {
        "field": "category.keyword",
        "size": 10
      }
    }
  }c
}
```

#### Range Aggregation
Değerleri belirli aralıklara böler.
```json
GET kibana_sample_data_ecommerce/_search
{
  "_source": false, 
  "aggs": {
    "price_range": {
      "range": {
        "field": "taxful_total_price",
        "ranges": [
          {
            "from": 50,
            "to": 100
          },
          {
            "from": 100,
            "to": 150
          }
        ]
      }
    }
  }
}
```

### Metric Aggregations
#### Avg Aggregation
Bir alanın ortalama değerini döndürür.
```json
GET kibana_sample_data_ecommerce/_search
{
  "_source": false,
  "query": {
    "term": {
      "category.keyword": {
        "value": "Men's Clothing"
      }
    }
  }, 
  "aggs": {
    "average_total_price": {
      "avg": {
        "field": "taxful_total_price"
      }
    }
  }
}
```

#### Sum/Max/Min Aggregations
- **Sum**: Toplam değer
- **Max**: En yüksek değer
- **Min**: En düşük değer

**Örnek:**
```json
GET kibana_sample_data_ecommerce/_search
{
  "_source": false, 
  "aggs": {
    "taxful_total_price_sum": {
      "sum": {
        "field": "taxful_total_price"
      }
    }
  } 
}
```
